using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelService;
using Serilog;
using DataService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace FiltersService
{
    public class AdminAuthenticationHandler : AuthenticationHandler<AdminAuthenticationOptions>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _provider;
        private readonly IdentityDefaultOptions _identityDefaultOptions;
        private readonly DataProtectionKeys _dataProtectionKeys;
        private readonly AppSettings _appSettings;
        private const string AccessToken = "access_token";
        private const string User_Id = "user_id";
        private const string Username = "username";
        private string[] UserRoles = new[] { "Administrator"};

        public AdminAuthenticationHandler(
            IOptionsMonitor<AdminAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> appSettings, IOptions<DataProtectionKeys> dataProtectionKeys,
            IServiceProvider provider,
            IOptions<IdentityDefaultOptions> identityDefaultOptions

            ) : base(options, logger, encoder, clock)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _identityDefaultOptions = identityDefaultOptions.Value;
            _provider = provider;
            _dataProtectionKeys = dataProtectionKeys.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Cookies.ContainsKey(AccessToken) || !Request.Cookies.ContainsKey(User_Id))
            {
                Log.Error("No Access Token or User Id found.");
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!AuthenticationHeaderValue.TryParse($"{"Bearer " + Request.Cookies[AccessToken]}",
               out AuthenticationHeaderValue headerValue))
            {
                Log.Error("Could not Parse Token from Authentication Header.");
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!AuthenticationHeaderValue.TryParse($"{"Bearer " + Request.Cookies[User_Id]}",
                out AuthenticationHeaderValue headerValueUid))
            {
                Log.Error("Could not Parse User Id from Authentication Header.");
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            try
            {
                /* STEP 1. Get the Validation Parameters for our applications JWT Token */
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                /* STEP 2. Create an instance of Jwt token handler */
                var handler = new JwtSecurityTokenHandler();

                /* STEP 3. Create an instance of Jwt token  validation parameters */
                TokenValidationParameters validationParameters =
                   new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidIssuer = _appSettings.Site,
                       ValidAudience = _appSettings.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };

                /* STEP 4. Get the Data protection service instance */
                var protectorProvider = _provider.GetService<IDataProtectionProvider>();

                /* STEP 5. create a protector instance */
                var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);

                /* STEP 6. Layer One Unprotect the user id */
                var decryptedUid = protector.Unprotect(headerValueUid.Parameter);

                /* STEP 7. Layer One Unprotect the user token */
                var decryptedToken = protector.Unprotect(headerValue.Parameter);

                /* STEP 8. Create an instance of the user tokenModel */
                TokenModel tokenModel = new TokenModel();

                /* STEP 9 Get the existing token for the user from Database using a scoped request */
               
                using (var scope = _provider.CreateScope())
                {
                    var dbContextService = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    var userToken = dbContextService.Tokens.Include(x => x.User)
                        .FirstOrDefault(ut => ut.UserId == decryptedUid
                                         && ut.User.UserName == Request.Cookies[Username]
                                         && ut.User.Id == decryptedUid
                                         && ut.User.UserRole == "Administrator");
                    tokenModel = userToken;
                }

                /* Step 11. Check if tokenmodel is null */
                if (tokenModel == null)
                {
                    return await Task.FromResult(AuthenticateResult.Fail("You are not authorized to View this Page"));
                }

                /* STEP 12. Apply second layer of decryption using the key store in the token model */
                /* STEP 12.1 Create Protector instance for layer two using token model key */
                /* IMPORTANT - If no key exists or key is invalid - exception will be thrown */
                IDataProtector layerTwoProtector = protectorProvider.CreateProtector(tokenModel?.EncryptionKeyJwt);
                string decryptedTokenLayerTwo = layerTwoProtector.Unprotect(decryptedToken);


                /* STEP 13. Validate the token we received - using validation parameters set in step 3 */
                /* IMPORTANT - If the validation fails - the method ValidateToken will throw exception */
                var validateToken = handler.ValidateToken(decryptedTokenLayerTwo, validationParameters, out var securityToken);

                /* STEP 14. Checking Token Signature */
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    return await Task.FromResult(AuthenticateResult.Fail("Your are not authorized"));
                }

                /* STEP 15. Extract the username from the validated token */
                var username = validateToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

                if (Request.Cookies[Username] != username)
                {
                    return await Task.FromResult(AuthenticateResult.Fail("You are not authorized to View this Page"));
                }


                /* STEP 16. Get User by their email */
                var user = await _userManager.FindByNameAsync(username);

                /* STEP 17. If user does not exist return authentication failed result */
                if (user == null)
                {
                    return await Task.FromResult(AuthenticateResult.Fail("You are not authorized to View this Page"));
                }

                /* STEP 18. We need to check if the user belongs to the group of user-roles */

                if (!UserRoles.Contains(user.UserRole))
                {
                    return await Task.FromResult(AuthenticateResult.Fail("You are not authorized to View this Page"));
                }

                /* STEP 19. Now we will create an authentication ticket, as the token is valid */
                var identity = new ClaimsIdentity(validateToken.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return await Task.FromResult(AuthenticateResult.Success(ticket));

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return await Task.FromResult(AuthenticateResult.Fail("Your are not authorized"));
            }

        }

        /* The purpose of this method is to handle the situation when the authentication fails. */
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            /* Logic handling failed requests */

            // Delete any invalid cookies
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("user_id");
            Response.Headers["WWW-Authenticate"] = $"Not Authorized";
            Response.Redirect(_identityDefaultOptions.AccessDeniedPath);

            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuthService;
using CMS_CORE_NG.Extensions;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService;
using Serilog;
using UserService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Controllers.Api.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : ControllerBase
    {        
        private readonly IUserSvc _userSvc;
        private readonly IEmailSvc _emailSvc;
        private readonly IAuthSvc _authSvc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string[] _cookiesToDelete = { "loginStatus", "access_token", "userRole", "username", "refreshToken" };

        public AccountController(IUserSvc userSvc, IEmailSvc emailSvc, IAuthSvc authSvc, IHttpContextAccessor httpContextAccessor)
        {
            _userSvc = userSvc;
            _emailSvc = emailSvc;
            _authSvc = authSvc;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _userSvc.RegisterUserAsync(model);

            if (result.Message.Equals("Success") && result.IsValid)
            {
                // Sending Confirmation Email
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { UserId = result.Data["User"].Id, Code = result.Data["Code"] }, protocol: HttpContext.Request.Scheme);

                await _emailSvc.SendEmailAsync(
                    result.Data["User"].Email,
                    "Thank you for Registration!",
                    callbackUrl,
                    "EmailConfirmation.html");

                Log.Information($"New User Created => {result.Data["User"].UserName}");

                return Ok(new { username = result.Data["User"].UserName, email = result.Data["User"].Email, status = 1, message = "Registration Successful" });
            }
            return BadRequest(new JsonResult(result.Data));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Auth([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var jwtToken = await _authSvc.Auth(model);

                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return Unauthorized(new { LoginError = jwtToken.ResponseInfo.Message });
                }
                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.BadRequest)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return BadRequest(new { LoginError = jwtToken.ResponseInfo.Message });
                }

                if (!jwtToken.TwoFactorLoginOn) return Ok(jwtToken);

                // Update the Response Message
                jwtToken.ResponseInfo.Message = "Auth Code Required";

                var twoFactorCodeModel = await _userSvc.GenerateTwoFactorCodeAsync(true, jwtToken.UserId);

                if (twoFactorCodeModel == null)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return BadRequest("Error");
                }

                if (twoFactorCodeModel.AuthCodeRequired)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return Unauthorized(new
                    {
                        LoginError = jwtToken.ResponseInfo.Message,
                        Expiry = twoFactorCodeModel.ExpiryDate,
                        twoFactorToken = twoFactorCodeModel.Token,
                        UserId = twoFactorCodeModel.UserId
                    });
                }

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return Unauthorized();

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authSvc.LogoutUserAsync();
            if (result)
            {
                return Ok(new { Message = "Logout Successful" });
            }

            return BadRequest(new { Message = "Invalid Request" });
        }

        [HttpPost("[action]/{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email)
        {
            if (ModelState.IsValid)
            {
                var result = await _userSvc.ForgotPassword(email);

                if (!result.IsValid)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok(new { Message = "Success" });
                }
               
                var callbackUrl = _httpContextAccessor.AbsoluteUrl("/api/v1/Account/ResetPassword", new { userId = (string)result.Data["User"].Id, code = (string)result.Data["Code"] });
                await _emailSvc.SendEmailAsync(
                    email,
                    "Reset Password",
                    callbackUrl,
                    "ForgotPasswordConfirmation.html");
                return Ok(new { Message = "Success" });
            }

            // If we got this far, something failed, redisplay form
            return BadRequest("We have Encountered an Error");
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            return RedirectToAction("ResetPassword", "Password", new ResetPasswordViewModel { Code = code });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendTwoFactorCode([FromBody] TwoFactorRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // First we need to check if this request is valid - We cannot depend on client side validation alone
            // Check the validity of TwoFactorToken & Session Expiry
            try
            {
                var result = await _userSvc.SendTwoFactorAsync(model);

                if (result.IsValid)
                {

                    // Send code to the user via to their preferred provider.
                    if (model.ProviderType.Equals("Email"))
                    {                       
                        var message = $"<h2>Your Two-Factor Authentication Code : {result.Code}</h2>";
                        await _emailSvc.SendEmailAsync(
                            result.Email,
                            "Two-Factor Code",
                            message,
                            "TwoFactorAuthentication.html");


                        return Ok(new { Message = "TwoFactorCode-Send" });
                    }
                    if (model.ProviderType.Equals("SMS"))
                    {
                        //TODO : Phase 2
                        return BadRequest("SMS Service not implemented");
                    }                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return Unauthorized(new { LoginError = "Two-Factor Fail" });
        }

        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> SessionExpiryNotification([FromRoute] string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _userSvc.ExpireUserSessionAsync(userId);

                if (result.IsValid)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return Ok(new {Message = "Success"});
                }
            }

            _authSvc.DeleteAllCookies(_cookiesToDelete);
            return BadRequest(new { Message = "Failed" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateTwoFactor([FromBody] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _userSvc.ValidateTwoFactorCodeAsync(code);

                if (!result.IsValid)
                {
                    return Unauthorized(new { LoginError = result.ResponseMessage.Message, AttemptsRemaining = result.Attempts });
                }

                var jwtToken = await _authSvc.GenerateNewToken();

                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.Unauthorized) return Unauthorized(new { LoginError = jwtToken.ResponseInfo.Message });

                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(jwtToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Unauthorized(new { LoginError = "You request cannot be completed" });
        }

    }
}
    
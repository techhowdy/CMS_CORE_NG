using System;
using System.Threading.Tasks;
using ActivityService;
using CookieService;
using DataService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using ModelService;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;

namespace UserService
{
    public class UserSvc : IUserSvc
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly ApplicationDbContext _db;
        private readonly ICookieSvc _cookieSvc;
        private readonly IActivitySvc _activitySvc;
        private readonly IServiceProvider _provider;
        private readonly DataProtectionKeys _dataProtectionKeys;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserSvc(
                    UserManager<ApplicationUser> userManager,
                    IHostingEnvironment env,
                    ApplicationDbContext db,
                    ICookieSvc cookieSvc,
                    IActivitySvc activitySvc,
                    IServiceProvider provider,
                    IOptions<DataProtectionKeys> dataProtectionKeys,
                    IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _env = env;
            _db = db;
            _cookieSvc = cookieSvc;
            _activitySvc = activitySvc;
            _dataProtectionKeys = dataProtectionKeys.Value;
            _provider = provider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject> RegisterUserAsync(RegisterViewModel model)
        {
            // Will hold all the errors related to registration
            var errorList = new List<string>();

            ResponseObject responseObject = new ResponseObject();

            try
            {
                var defaultProfilePicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}default{Path.DirectorySeparatorChar}profile.jpeg";


                // Create the Profile Image Path
                var profPicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}";

                var extension = ".jpeg";
                var filename = DateTime.Now.ToString("yymmssfff");
                var path = Path.Combine(profPicPath, filename) + extension;
                var dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}", filename) + extension;

                File.Copy(defaultProfilePicPath, path);

                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Username,
                    UserRole = "Customer",
                    PhoneNumber = model.Phone,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Gender = model.Gender,
                    Terms = model.Terms,
                    IsProfileComplete = false,
                    Birthday = model.Dob,
                    ProfilePic = dbImagePath,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel {Country = model.Country, Type = "Billing"},
                        new AddressModel {Country = model.Country, Type = "Shipping"}
                    }
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var dynamicProperties = new Dictionary<string, object> { ["Code"] = code, ["User"] = user };

                    responseObject.IsValid = true;
                    responseObject.Message = "Success";

                    responseObject.Data = dynamicProperties;
                    return responseObject;
                }

                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }
                responseObject.IsValid = false;
                responseObject.Message = "Failed";
                responseObject.Data = errorList;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while registering new user  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return responseObject;
        }

        public async Task<ProfileModel> GetUserProfileByIdAsync(string userId)
        {
            ProfileModel userProfile = new ProfileModel();

            var loggedInUserId = GetLoggedInUserId();

            var user = await _userManager.FindByIdAsync(loggedInUserId);

            if (user == null || user.Id != userId) return null;

            try
            {
                userProfile = new ProfileModel()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    Phone = user.PhoneNumber,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Displayname = user.DisplayName,
                    Firstname = user.Firstname,
                    Middlename = user.Middlename,
                    Lastname = user.Lastname,
                    IsEmailVerified = user.EmailConfirmed,
                    IsPhoneVerified = user.PhoneNumberConfirmed,
                    IsTermsAccepted = user.Terms,
                    IsTwoFactorOn = user.TwoFactorEnabled,
                    ProfilePic = user.ProfilePic,
                    UserRole = user.UserRole,
                    IsAccountLocked = user.LockoutEnabled,
                    IsEmployee = user.IsEmployee,
                    UseAddress = new List<AddressModel>(await _db.Addresses.Where(x => x.UserId == user.Id).Select(n =>
                        new AddressModel()
                        {
                            AddressId = n.AddressId,
                            Line1 = n.Line1,
                            Line2 = n.Line2,
                            Unit = n.Unit,
                            Country = n.Country,
                            State = n.State,
                            City = n.City,
                            PostalCode = n.PostalCode,
                            Type = n.Type,
                            UserId = n.UserId
                        }).ToListAsync()),
                    Activities = new List<ActivityModel>(_db.Activities.Where(x => x.UserId == user.Id)).OrderByDescending(o => o.Date).Take(20).ToList()
                };
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return userProfile;


        }

        public async Task<ProfileModel> GetUserProfileByUsernameAsync(string username)
        {
            var userProfile = new ProfileModel();

            try
            {
                var loggedInUserId = GetLoggedInUserId();
                var user = await _userManager.FindByIdAsync(loggedInUserId);
                if (user == null || user.UserName != username) return null;

                userProfile = new ProfileModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    Phone = user.PhoneNumber,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Displayname = user.DisplayName,
                    Firstname = user.Firstname,
                    Middlename = user.Middlename,
                    Lastname = user.Lastname,
                    IsEmailVerified = user.EmailConfirmed,
                    IsPhoneVerified = user.PhoneNumberConfirmed,
                    IsTermsAccepted = user.Terms,
                    IsTwoFactorOn = user.TwoFactorEnabled,
                    ProfilePic = user.ProfilePic,
                    UserRole = user.UserRole,
                    IsAccountLocked = user.LockoutEnabled,
                    IsEmployee = user.IsEmployee,
                    UseAddress = new List<AddressModel>(await _db.Addresses.Where(x => x.UserId == user.Id).Select(n =>
                        new AddressModel()
                        {
                            AddressId = n.AddressId,
                            Line1 = n.Line1,
                            Line2 = n.Line2,
                            Unit = n.Unit,
                            Country = n.Country,
                            State = n.State,
                            City = n.City,
                            PostalCode = n.PostalCode,
                            Type = n.Type,
                            UserId = n.UserId
                        }).ToListAsync()),
                    Activities = new List<ActivityModel>(_db.Activities.Where(x => x.UserId == user.Id)).OrderByDescending(o => o.Date).Take(20).ToList()
                };

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return userProfile;
        }

        public async Task<ProfileModel> GetUserProfileByEmailAsync(string email)
        {
            var userProfile = new ProfileModel();

            try
            {
                var loggedInUserId = GetLoggedInUserId();
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user == null || user.Email != email) return null;

                userProfile = new ProfileModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    Phone = user.PhoneNumber,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Displayname = user.DisplayName,
                    Firstname = user.Firstname,
                    Middlename = user.Middlename,
                    Lastname = user.Lastname,
                    IsEmailVerified = user.EmailConfirmed,
                    IsPhoneVerified = user.PhoneNumberConfirmed,
                    IsTermsAccepted = user.Terms,
                    IsTwoFactorOn = user.TwoFactorEnabled,
                    ProfilePic = user.ProfilePic,
                    UserRole = user.UserRole,
                    IsAccountLocked = user.LockoutEnabled,
                    IsEmployee = user.IsEmployee,
                    UseAddress = new List<AddressModel>(await _db.Addresses.Where(x => x.UserId == user.Id).Select(n =>
                        new AddressModel()
                        {
                            AddressId = n.AddressId,
                            Line1 = n.Line1,
                            Line2 = n.Line2,
                            Unit = n.Unit,
                            Country = n.Country,
                            State = n.State,
                            City = n.City,
                            PostalCode = n.PostalCode,
                            Type = n.Type,
                            UserId = n.UserId
                        }).ToListAsync()),
                    Activities = new List<ActivityModel>(_db.Activities.Where(x => x.UserId == user.Id)).OrderByDescending(o => o.Date).Take(20).ToList()
                };

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return userProfile;
        }

        public async Task<bool> CheckPasswordAsync(ProfileModel model, string password)
        {
            try
            {
                var loggedInUserId = GetLoggedInUserId();
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user.UserName != _cookieSvc.Get("username") ||
                    user.UserName != model.Username)
                    return false;

                if (!await _userManager.CheckPasswordAsync(user, password))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateProfileAsync(IFormCollection formData)
        {
            var loggedInUserId = GetLoggedInUserId();
            var user = await _userManager.FindByIdAsync(loggedInUserId);

            if (user == null) return false;

            if (user.UserName != _cookieSvc.Get("username") ||
                user.UserName != formData["username"].ToString() ||
                user.Email != formData["email"].ToString())
                return false;

            try
            {
                ActivityModel activityModel = new ActivityModel { UserId = user.Id };
                await UpdateProfilePicAsync(formData, user);
               
                user.Firstname = formData["firstname"];
                user.Birthday = formData["birthdate"];
                user.Lastname = formData["lastname"];
                user.Middlename = formData["middlename"];
                user.DisplayName = formData["displayname"];
                user.PhoneNumber = formData["phone"];
                user.Gender = formData["gender"];
                user.TwoFactorEnabled = Convert.ToBoolean(formData["IsTwoFactorOn"]);                

                /* If Addresses exist we update them => If Addresses do not exist we add them */
                await InsertOrUpdateAddress(user.Id, "Shipping", formData["saddress1"], formData["saddress2"], formData["scountry"], formData["sstate"], formData["scity"], formData["spostalcode"], formData["sunit"]);
                await InsertOrUpdateAddress(user.Id, "Billing", formData["address1"], formData["address2"], formData["country"], formData["state"], formData["city"], formData["postalcode"], formData["unit"]);

                await _userManager.UpdateAsync(user);

                activityModel.Date = DateTime.UtcNow;
                activityModel.IpAddress = _cookieSvc.GetUserIP();
                activityModel.Location = _cookieSvc.GetUserCountry();
                activityModel.OperatingSystem = _cookieSvc.GetUserOS();
                activityModel.Type = "Profile update successful";
                activityModel.Icon = "fas fa-thumbs-up";
                activityModel.Color = "success";
                await _activitySvc.AddUserActivity(activityModel);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while updating profile {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return false;
        }

        public async Task<bool> AddUserActivity(ActivityModel model)
        {
            try
            {
                await _activitySvc.AddUserActivity(model);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return false;
        }

        public async Task<bool> ChangePasswordAsync(ProfileModel model, string newPassword)
        {
            bool result;
            try
            {
                ActivityModel activityModel = new ActivityModel();
                activityModel.Date = DateTime.UtcNow;
                activityModel.IpAddress = _cookieSvc.GetUserIP();
                activityModel.Location = _cookieSvc.GetUserCountry();
                activityModel.OperatingSystem = _cookieSvc.GetUserOS();

                var loggedInUserId = GetLoggedInUserId();
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                    var updateResult = await _userManager.UpdateAsync(user);
                    result = updateResult.Succeeded;
                    activityModel.UserId = user.Id;
                    activityModel.Type = "Password Changed successful";
                    activityModel.Icon = "fas fa-thumbs-up";
                    activityModel.Color = "success";
                    await _activitySvc.AddUserActivity(activityModel);                    
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                result = false;
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return result;
        }

        public async Task<List<ActivityModel>> GetUserActivity(string username)
        {
            List<ActivityModel> userActivities = new List<ActivityModel>();

            try
            {
                var loggedInUserId = GetLoggedInUserId();
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user == null || user.UserName != username) return null;

                userActivities = await _db.Activities.Where(x => x.UserId == user.Id).OrderByDescending(o => o.Date).Take(20).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return userActivities;
        }

        public async Task<ResponseObject> ForgotPassword(string email)
        {
            ResponseObject responseObject = new ResponseObject();

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    responseObject.Message = "Failed";
                    responseObject.IsValid = false;
                    responseObject.Data = null;
                    return responseObject;
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                responseObject.Message = "Success";
                responseObject.IsValid = true;
                var dynamicProperties = new Dictionary<string, object> { ["Code"] = code, ["User"] = user };
                responseObject.Data = dynamicProperties;
                return responseObject;

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ForgotPassword {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                responseObject.Message = "Error";
                responseObject.IsValid = false;
                responseObject.Data = null;
                return responseObject;
            }
        }

        public async Task<ResponseObject> ResetPassword(ResetPasswordViewModel model)
        {
            ResponseObject responseObject = new ResponseObject();
            ActivityModel activityModel = new ActivityModel
            {
                Date = DateTime.UtcNow,
                IpAddress = _cookieSvc.GetUserIP(),
                Location = _cookieSvc.GetUserCountry(),
                OperatingSystem = _cookieSvc.GetUserOS()
            };

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    responseObject.Message = "Failed";
                    responseObject.IsValid = false;
                    responseObject.Data = null;
                    return responseObject;
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    activityModel.UserId = user.Id;
                    activityModel.Type = "Password Changed successful";
                    activityModel.Icon = "fas fa-key";
                    activityModel.Color = "warning";
                    await _activitySvc.AddUserActivity(activityModel);
                    responseObject.Message = "Success";
                    responseObject.IsValid = true;
                    responseObject.Data = null;
                    return responseObject;
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ForgotPassword {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            responseObject.Message = "Failed";
            responseObject.IsValid = false;
            responseObject.Data = null;
            return responseObject;
        }

        public async Task<TwoFactorResponseModel> SendTwoFactorAsync(TwoFactorRequestModel model)
        {
            var twoFactorResponse = new TwoFactorResponseModel();
            var result = new TwoFactorCodeModel();

            try
            {
                var protectorProvider = _provider.GetService<IDataProtectionProvider>();

                var userIdFromHeader = _httpContextAccessor.HttpContext.Request.Headers["user_id"];
                var twoFactorToken = model.TwoFactorToken;

                // If two factor token is null we dont want to further execute this method
                if (!string.IsNullOrEmpty(twoFactorToken) && !string.IsNullOrEmpty(userIdFromHeader))
                {
                    var userId = DecryptData(userIdFromHeader, _dataProtectionKeys.ApplicationUserKey).ToString();
                    // First find user with that Id id two-factor Table
                    // If user was found, check if the token is valid or expired
                    var userResult = await _db.TwoFactorCodes.Where(x =>
                        x.UserId == userId &&
                        x.CodeExpired == false &&
                        x.CodeIsUsed == false &&
                        x.ExpiryDate > DateTime.UtcNow).FirstOrDefaultAsync();

                    if (userResult != null)
                    {
                        // Decrypted Two-Factor-Token from request
                        var protector = protectorProvider.CreateProtector(userResult.EncryptionKey2Fa);
                        var decryptedTwoFactorToken = protector.Unprotect(twoFactorToken);

                        // Get the Application User
                        var appUser = await _userManager.FindByIdAsync(userId);

                        // If both the values match request vs DB
                        if (userResult.Token != null && decryptedTwoFactorToken != null && userResult.Token == decryptedTwoFactorToken)
                        {
                            // check in current request if they want to remember
                            userResult.RememberDevice = model.RememberDevice;
                            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
                            _db.Entry(userResult).State = EntityState.Modified;
                            await _db.SaveChangesAsync();
                            await dbContextTransaction.CommitAsync();

                            twoFactorResponse.IsValid = true;
                            twoFactorResponse.Email = appUser.Email;
                            twoFactorResponse.Code = userResult.TwoFactorCode;
                            twoFactorResponse.RememberDevice = userResult.RememberDevice;
                            twoFactorResponse.ResponseMessage = new ResponseStatusInfoModel
                            {
                                Message = "Authentication Success",
                                StatusCode = HttpStatusCode.OK
                            };
                            protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
                            var protectedUserId = protector.Protect(userId);
                            _cookieSvc.SetCookie("user_id", protectedUserId, Convert.ToInt32(userResult.ExpiryDate.Subtract(DateTime.UtcNow).TotalMinutes));
                            return twoFactorResponse;
                        }
                    }

                }
                twoFactorResponse.IsValid = false;
                twoFactorResponse.Email = string.Empty;
                twoFactorResponse.Code = string.Empty;
                twoFactorResponse.RememberDevice = false;
                twoFactorResponse.ResponseMessage = new ResponseStatusInfoModel
                {
                    Message = "Authentication Failed",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ValidateTwoFactorAsync {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return twoFactorResponse;
        }

        public async Task<TwoFactorCodeModel> GenerateTwoFactorCodeAsync(bool authRequired, string userId)
        {
            try
            {
                var protectorProvider = _provider.GetService<IDataProtectionProvider>();
                var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
                var unprotectUserId = protector.Unprotect(userId);
                var user = await _userManager.FindByIdAsync(unprotectUserId);

                if (user != null)
                {
                    var twoFactorToken = Guid.NewGuid().ToString("N"); // Token for two factor
                    var deviceIdToken = Guid.NewGuid().ToString("N");  // token for Device Id

                    var keyTwoFactorToken = Guid.NewGuid().ToString("N"); // Encryption key for TwoFactorToken & DeviceIdToken
                    var keyDeviceIdToken = Guid.NewGuid().ToString("N");  // token for Device Id

                    var protectorTwoFactorToken = protectorProvider.CreateProtector(keyTwoFactorToken);
                    var protectorDeviceIdToken = protectorProvider.CreateProtector(keyDeviceIdToken);

                    var protectedTwoFactorToken = protectorTwoFactorToken.Protect(twoFactorToken);
                    var protectedDeviceIdTokenToken = protectorDeviceIdToken.Protect(deviceIdToken);

                    // Cookie Expiry time 
                    int expiryTime = !user.RememberMe ? 5 : 600;


                    var twoFactorCodeModel = await GetTwoFactorCodeModel(authRequired, user, twoFactorToken, deviceIdToken, keyTwoFactorToken, keyDeviceIdToken, expiryTime);

                    // Before adding new token we delete old token
                    var oldToken = await _db.TwoFactorCodes.Where(x => x.UserId == user.Id).ToListAsync();

                    if (oldToken.Count > 0)
                    {
                        _db.TwoFactorCodes.RemoveRange(oldToken);
                        await _db.SaveChangesAsync();
                    }

                    // Save the new new token in Database
                    await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
                    await _db.TwoFactorCodes.AddAsync(twoFactorCodeModel);
                    await _db.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    twoFactorCodeModel.Token = protectedTwoFactorToken;
                    twoFactorCodeModel.DeviceId = protectedDeviceIdTokenToken;
                    twoFactorCodeModel.UserId = EncryptData<string>(twoFactorCodeModel.UserId, _dataProtectionKeys.ApplicationUserKey);
                    return twoFactorCodeModel;
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while generating two-factor code  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return null;
        }

        public async Task<ResponseObject> ExpireUserSessionAsync(string userId)
        {
            ResponseObject responseObject = new ResponseObject();
            responseObject.Message = "Failed";
            responseObject.IsValid = false;
            responseObject.Data = null;

            ActivityModel activityModel = new ActivityModel
            {
                Date = DateTime.UtcNow,
                IpAddress = _cookieSvc.GetUserIP(),
                Location = _cookieSvc.GetUserCountry(),
                OperatingSystem = _cookieSvc.GetUserOS()
            };

            try
            {
                var decryptedUserId = DecryptData(userId, _dataProtectionKeys.ApplicationUserKey).ToString();

                var user = await _userManager.FindByIdAsync(decryptedUserId);

                if (user != null)
                {
                    var result = await DeleteUserTokensAsync(user.Id);

                    if (result)
                    {
                        activityModel.UserId = user.Id;
                        activityModel.Type = "Session Expired";
                        activityModel.Icon = "fas fa-clock";
                        activityModel.Color = "danger";
                        await _activitySvc.AddUserActivity(activityModel);
                        responseObject.Message = "Success";
                        responseObject.IsValid = true;
                        responseObject.Data = null;

                        return responseObject;
                    }                    

                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while generating two-factor code  {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return responseObject;
        }

        public async Task<TwoFactorResponseModel> ValidateTwoFactorCodeAsync(string code)
        {
            // Initially setting this to Fail
            var response = new TwoFactorResponseModel
            {
                Code = string.Empty,
                Email = string.Empty,
                IsValid = false,
                RememberDevice = false,
                ResponseMessage = new ResponseStatusInfoModel
                {
                    Message = "Code-InValid",
                    StatusCode = HttpStatusCode.Unauthorized
                }
            };

            try
            {
                string twoFactorToken = _httpContextAccessor.HttpContext.Request.Headers["twoFactorToken"];

                var loggedInUserId = GetLoggedInUserId();
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user != null && twoFactorToken != null && !string.IsNullOrEmpty(code))
                {
                    var userResult = await _db.TwoFactorCodes.Where(x =>
                        x.UserId == user.Id &&
                        x.CodeExpired == false &&
                        x.ExpiryDate > DateTime.UtcNow &&
                        !x.CodeIsUsed && x.Attempts <= 3).FirstOrDefaultAsync();

                    if (userResult != null)
                    {
                        // Check Code attempts
                        if (userResult.TwoFactorCode != code)
                        {
                            userResult.Attempts -= 1;
                            _db.Update(userResult);
                            await _db.SaveChangesAsync();
                            response.Attempts = userResult.Attempts;
                            _cookieSvc.SetCookie("user_id", EncryptData<string>(loggedInUserId, _dataProtectionKeys.ApplicationUserKey), Convert.ToInt32(userResult.ExpiryDate.Subtract(DateTime.UtcNow).TotalMinutes));
                            if (userResult.Attempts == 0)
                            {
                                await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
                                user.LockoutEnabled = true;
                                _db.Entry(user).State = EntityState.Modified;
                                await _db.SaveChangesAsync();
                                await dbContextTransaction.CommitAsync();
                            }

                            return response;
                        }

                        var decryptedTwoFactorToken = DecryptData(twoFactorToken, userResult.EncryptionKey2Fa);

                        if (decryptedTwoFactorToken == userResult.Token)
                        {
                            // Before sending true we need to set the code as Expired
                            // reset the two-factor again 
                            userResult.CodeExpired = true;
                            userResult.CodeIsUsed = true;
                            response.Code = userResult.TwoFactorCode;
                            response.Email = user.Email;
                            response.IsValid = true;
                            response.RememberDevice = false;
                            response.ResponseMessage = new ResponseStatusInfoModel
                            {
                                Message = "Code-Valid",
                                StatusCode = HttpStatusCode.OK
                            };
                            return response;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ValidateTwoFactorAsync {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return response;
        }

        private async Task<TwoFactorCodeModel> GetTwoFactorCodeModel(bool authRequired, ApplicationUser user, string twoFactorToken, string deviceIdToken, string keyTwoFactorToken, string keyDeviceIdToken, int expiryTime)
        {
            return new TwoFactorCodeModel()
            {
                UserId = user.Id,
                Token = twoFactorToken,
                TwoFactorCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Email"),
                ExpiryDate = DateTime.UtcNow.AddMinutes(5),
                CreatedDate = DateTime.UtcNow,
                EncryptionKey2Fa = keyTwoFactorToken,
                RememberDevice = user.RememberMe,
                SelectedProvider = string.Empty,
                DeviceId = deviceIdToken,
                EncryptionKeyForDeviceId = keyDeviceIdToken,
                IsDeviceIdExpired = false,
                Attempts = 3,
                IpAddress = _cookieSvc.GetUserIP(),
                CodeExpired = (!authRequired),
                CodeIsUsed = (!authRequired),
                UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
                DeviceIdExpiry = DateTime.UtcNow.AddMinutes(expiryTime),
                AuthCodeRequired = authRequired
            };
        }

        private T DecryptData<T>(T encryptedData, string key)
        {
            dynamic decryptedData = null;
            var protectorProvider = _provider.GetService<IDataProtectionProvider>();
            var protector = protectorProvider.CreateProtector(key);

            if (encryptedData != null)
            {
                decryptedData = protector.Unprotect(encryptedData.ToString());
            }

            return decryptedData;
        }

        private string EncryptData<T>(string data, string key)
        {
            var protectorProvider = _provider.GetService<IDataProtectionProvider>();
            var protector = protectorProvider.CreateProtector(key);
            if (data != null)
            {
                var encryptedData = protector.Protect(data);
                return encryptedData;
            }
            return null;
        }

        private string GetLoggedInUserId()
        {
            try
            {
                var protectorProvider = _provider.GetService<IDataProtectionProvider>();
                var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
                var unprotectUserId = protector.Unprotect(_cookieSvc.Get("user_id"));
                return unprotectUserId;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while decrypting user Id  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return null;

        }

        private async Task<ApplicationUser> UpdateProfilePicAsync(IFormCollection formData, ApplicationUser user)
        {
            // First we create an empty array to store old file info
            var oldProfilePic = new string[1];
            // we will store path of old file to delete in an empty array.
            oldProfilePic[0] = Path.Combine(_env.WebRootPath + user.ProfilePic);

            // Create the Profile Image Path
            var profPicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}";

            // If we have received any files for update, then we update the file path after saving to server
            // else we return the user without any changes
            if (formData.Files.Count <= 0) return user;

            var extension = Path.GetExtension(formData.Files[0].FileName);
            var filename = DateTime.Now.ToString("yymmssfff");
            var path = Path.Combine(profPicPath, filename) + extension;
            var dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}", filename) + extension;

            user.ProfilePic = dbImagePath;

            // Copying New Files to the Server - profile Folder
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await formData.Files[0].CopyToAsync(stream);
            }

            // Delete old file after successful update
            if (!System.IO.File.Exists(oldProfilePic[0])) return user;

            System.IO.File.SetAttributes(oldProfilePic[0], FileAttributes.Normal);
            System.IO.File.Delete(oldProfilePic[0]);

            return user;
        }

        private async Task InsertOrUpdateAddress(string userId, string type, string line1, string line2, string country,
            string state, string city, string postalcode, string unit)
        {
            var updateAddress = _db.Addresses.FirstOrDefault(ad => ad.User.Id == userId && ad.Type == type);
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var newAddress = new AddressModel();
                if (updateAddress != null)
                {
                    updateAddress.Line1 = line1;
                    updateAddress.Line2 = line2;
                    updateAddress.Country = country;
                    updateAddress.City = city;
                    updateAddress.State = state;
                    updateAddress.PostalCode = postalcode;
                    updateAddress.Unit = unit;
                    _db.Entry(updateAddress).State = EntityState.Modified;
                }
                else
                {
                    newAddress.Line1 = line1;
                    newAddress.Line2 = line2;
                    newAddress.Country = country;
                    newAddress.City = city;
                    newAddress.State = state;
                    newAddress.PostalCode = postalcode;
                    newAddress.Unit = unit;
                    _db.Entry(newAddress).State = EntityState.Added;
                }

                await _db.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }

        private async Task<bool> DeleteUserTokensAsync(string userId)
        {
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
            var result = false;

            try
            {
                var oldToken = await _db.TwoFactorCodes.Where(x => x.UserId == userId).ToListAsync();
                var oldJwtoken = await _db.Tokens.Where(x => x.UserId == userId).ToListAsync();

                if (oldToken.Count > 0)
                {
                    _db.TwoFactorCodes.RemoveRange(oldToken);                    
                }
                if (oldJwtoken.Count > 0)
                {
                    _db.Tokens.RemoveRange(oldJwtoken);
                }
                await _db.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return result;
        }
    }
}

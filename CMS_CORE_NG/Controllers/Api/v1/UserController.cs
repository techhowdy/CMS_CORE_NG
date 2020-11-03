using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackendService;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    [Authorize(AuthenticationSchemes = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class UserController : ControllerBase
    {
        private readonly IUserSvc _userSvc;
        private readonly IWebHostEnvironment _env;
        private readonly IAdminSvc _adminSvc;
        private readonly IEmailSvc _emailSvc;


        public UserController(IUserSvc userSvc, IWebHostEnvironment env, IAdminSvc adminSvc, IEmailSvc emailSvc)
        {
            _userSvc = userSvc;
            _env = env;
            _adminSvc = adminSvc;
            _emailSvc = emailSvc;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminSvc.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("[action]/{username}")]
        public async Task<IActionResult> GetUserByUserName([FromRoute] string username)
        {
            try
            {
                var result = await _adminSvc.GetUserProfileByUsernameAsync(username);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

                return BadRequest("Error Occured");
            }
            return NotFound("User Not Found");
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(IFormCollection formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                // Creating Default image values
                var defaultProfilePicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}default{Path.DirectorySeparatorChar}profile.jpeg";
                // Create the Profile Image Path
                var profPicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}";

                var extension = ".jpeg";
                var filename = DateTime.Now.ToString("yymmssfff");
                var path = Path.Combine(profPicPath, filename) + extension;
                var dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}", filename) + extension;

                if (formData.Files.Count > 0)
                {
                    path = Path.Combine(profPicPath, filename) + extension;
                    await using var stream = new FileStream(path, FileMode.Create);
                    await formData.Files[0].CopyToAsync(stream);
                }
                if (formData.Files.Count <= 0)
                {
                    System.IO.File.Copy(defaultProfilePicPath, path);
                }

                var user = new ApplicationUser
                {
                    Email = formData["Email"],
                    UserName = formData["Username"],
                    PhoneNumber = formData["Phone"],
                    Birthday = formData["Birthday"],
                    Gender = formData["Gender"],
                    DisplayName = formData["DisplayName"],
                    Firstname = formData["FirstName"],
                    Middlename = formData["MiddleName"],
                    Lastname = formData["LastName"],
                    UserRole = formData["UserRole"],
                    LockoutEnabled = (Convert.ToInt32(formData["IsAccountLocked"]) != 0),
                    IsProfileComplete = (Convert.ToInt32(formData["IsProfileComplete"]) != 0),
                    PhoneNumberConfirmed = (Convert.ToInt32(formData["IsPhoneVerified"]) != 0),
                    EmailConfirmed = (Convert.ToInt32(formData["IsEmailVerified"]) != 0),
                    TwoFactorEnabled = (Convert.ToInt32(formData["IsTwoFactorOn"]) != 0),
                    Terms = (Convert.ToInt32(formData["IsTermsAccepted"]) != 0),
                    IsEmployee = (Convert.ToInt32(formData["IsEmployee"]) != 0),
                    IsActive = true,
                    ProfilePic = dbImagePath,
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel
                        {
                            Line1 = string.IsNullOrEmpty(formData["BillingAddress.Line1"])
                                ? ""
                                : (string) formData["BillingAddress.Line1"],
                            Line2 = string.IsNullOrEmpty(formData["BillingAddress.Line2"])
                                ? ""
                                : (string) formData["BillingAddress.Line2"],
                            Unit = string.IsNullOrEmpty(formData["BillingAddress.Unit"])
                                ? ""
                                : (string) formData["BillingAddress.Unit"],
                            City = string.IsNullOrEmpty(formData["BillingAddress.City"])
                                ? ""
                                : (string) formData["BillingAddress.City"],
                            Country = string.IsNullOrEmpty(formData["BillingAddress.Country"])
                                ? ""
                                : (string) formData["BillingAddress.Country"],
                            State = string.IsNullOrEmpty(formData["BillingAddress.State"])
                                ? ""
                                : (string) formData["BillingAddress.State"],
                            PostalCode = string.IsNullOrEmpty(formData["BillingAddress.PostalCode"])
                                ? ""
                                : (string) formData["BillingAddress.PostalCode"],
                            Type = "Billing"
                        },
                        new AddressModel
                        {
                            Line1 = string.IsNullOrEmpty(formData["ShippingAddress.Line1"])
                                ? ""
                                : (string) formData["ShippingAddress.Line1"],
                            Line2 = string.IsNullOrEmpty(formData["ShippingAddress.Line2"])
                                ? ""
                                : (string) formData["ShippingAddress.Line2"],
                            Unit = string.IsNullOrEmpty(formData["ShippingAddress.Unit"])
                                ? ""
                                : (string) formData["ShippingAddress.Unit"],
                            City = string.IsNullOrEmpty(formData["ShippingAddress.City"])
                                ? ""
                                : (string) formData["ShippingAddress.City"],
                            Country = string.IsNullOrEmpty(formData["ShippingAddress.Country"])
                                ? ""
                                : (string) formData["ShippingAddress.Country"],
                            State = string.IsNullOrEmpty(formData["ShippingAddress.State"])
                                ? ""
                                : (string) formData["ShippingAddress.State"],
                            PostalCode = string.IsNullOrEmpty(formData["ShippingAddress.PostalCode"])
                                ? ""
                                : (string) formData["ShippingAddress.PostalCode"],
                            Type = "Shipping"
                        }
                    }
                };
                var result = await _adminSvc.AddUserAsync(user, formData["Password"]);

                if (result.ContainsKey("Success"))
                {
                    return Ok("Success");
                }

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.SetAttributes(path, FileAttributes.Normal);
                    System.IO.File.Delete(path);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
        }

        [HttpPut("[action]/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser([FromRoute] string userId, IFormCollection formData)
        {
            try
            {
                var result = await _adminSvc.UpdateProfileAsync(userId, formData);

                if (result)
                {
                    return Ok("Success");
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return BadRequest("Failed");
        }

        [HttpDelete("[action]/{username}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser([FromRoute] string username)
        {
            try
            {
                var result = await _adminSvc.DeleteUserAsync(username);

                if (result)
                {
                    Log.Information("{Info}", $"User {username}'s account has been deactivated.");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("[action]/{username}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromRoute] string username)
        {
            var result = await _adminSvc.ResetPasswordAsync(username);
            if (result == null) return BadRequest("Failed to Change Password. Please check logs for more details.");

            // Send email with the new password to the user
            await _emailSvc.SendEmailAsync(
               result[0],
               "Password has been Reset",
               $"<p>New Password : {result[1]}</p>",
               "ResetPassword.html");

            return Ok("Password Changed Successfully");

        }
    }
}

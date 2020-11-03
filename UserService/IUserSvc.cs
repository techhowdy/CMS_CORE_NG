using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ModelService;

namespace UserService
{
    public interface IUserSvc
    {
        Task<ProfileModel> GetUserProfileByIdAsync(string userId);
        Task<ProfileModel> GetUserProfileByUsernameAsync(string username);
        Task<ProfileModel> GetUserProfileByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ProfileModel model, string password);
        Task<bool> UpdateProfileAsync(IFormCollection formData);
        Task<bool> AddUserActivity(ActivityModel model);
        Task<bool> ChangePasswordAsync(ProfileModel model, string newPassword);
        Task<List<ActivityModel>> GetUserActivity(string username);
        Task<ResponseObject> RegisterUserAsync(RegisterViewModel model);
        Task<ResponseObject> ForgotPassword(string email);
        Task<ResponseObject> ResetPassword(ResetPasswordViewModel model);
        Task<TwoFactorResponseModel> SendTwoFactorAsync(TwoFactorRequestModel model);
        Task<TwoFactorCodeModel> GenerateTwoFactorCodeAsync(bool authRequired, string userId);
        Task<ResponseObject> ExpireUserSessionAsync(string userId);
        Task<TwoFactorResponseModel> ValidateTwoFactorCodeAsync(string code);        
    }
}

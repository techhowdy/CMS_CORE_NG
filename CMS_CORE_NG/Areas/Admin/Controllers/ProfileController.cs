using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using ModelService;
using UserService;
using Microsoft.AspNetCore.Authorization;
using WritableOptionsService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {
        private readonly ICookieSvc _cookieSvc;
        private readonly IServiceProvider _provider;
        private readonly DataProtectionKeys _dataProtectionKeys;
        private readonly AppSettings _appSettings;
        private readonly IUserSvc _userSvc;
        private readonly IWritableSvc<SiteWideSettings> _writableSiteWideSettings;
        private static AdminBaseViewModel _adminBaseViewModel;

        public ProfileController(
            IUserSvc userSvc,
            ICookieSvc cookieSvc,
            IServiceProvider provider,
            IOptions<DataProtectionKeys> dataProtectionKeys,
            IOptions<AppSettings> appSettings, IWritableSvc<SiteWideSettings> writableSiteWideSettings)
        {

            _userSvc = userSvc;
            _cookieSvc = cookieSvc;
            _provider = provider;
            _dataProtectionKeys = dataProtectionKeys.Value;
            _appSettings = appSettings.Value;
            _writableSiteWideSettings = writableSiteWideSettings;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await SetAdminBaseViewModel();
            return View("Index", _adminBaseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Security()
        {
            await SetAdminBaseViewModel();
            return View("Security", _adminBaseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Activity()
        {
            await SetAdminBaseViewModel();
            return View("Activity", _adminBaseViewModel);
        }


        private async Task SetAdminBaseViewModel()
        {
            var protectorProvider = _provider.GetService<IDataProtectionProvider>();
            var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
            var userProfile = await _userSvc.GetUserProfileByIdAsync(protector.Unprotect(_cookieSvc.Get("user_id")));
            var resetPassword = new ResetPasswordViewModel();

            _adminBaseViewModel = new AdminBaseViewModel
            {
                Profile = userProfile,
                AddUser = null,
                AppSetting = null,
                Dashboard = null,
                ResetPassword = resetPassword,
                SiteWideSetting = _writableSiteWideSettings.Value
            };
           
        }

    }
}

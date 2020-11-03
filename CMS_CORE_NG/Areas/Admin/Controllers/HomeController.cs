using System;
using System.Threading.Tasks;
using CookieService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using ModelService;
using UserService;
using WritableOptionsService;
using DashboardService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {

        private readonly IUserSvc _userSvc;
        private readonly ICookieSvc _cookieSvc;
        private readonly IServiceProvider _provider;
        private readonly DataProtectionKeys _dataProtectionKeys;
        private readonly AppSettings _appSettings;
        private readonly IDashboardSvc _dashboardSvc;
        private readonly IWritableSvc<SiteWideSettings> _writableSiteWideSettings;
        private AdminBaseViewModel _adminBaseViewModel;

        public HomeController(IUserSvc userSvc, ICookieSvc cookieSvc,
            IServiceProvider provider,
            IOptions<DataProtectionKeys> dataProtectionKeys, IOptions<AppSettings> appSettings,
            IWritableSvc<SiteWideSettings> writableSiteWideSettings, IDashboardSvc dashboardSvc)
        {
            _userSvc = userSvc;
            _cookieSvc = cookieSvc;
            _provider = provider;
            _dataProtectionKeys = dataProtectionKeys.Value;
            _appSettings = appSettings.Value;
            _writableSiteWideSettings = writableSiteWideSettings;
            _dashboardSvc = dashboardSvc;
        }

        public async Task<IActionResult> Index()
        {
            var protectorProvider = _provider.GetService<IDataProtectionProvider>();
            var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
            var userProfile = await _userSvc.GetUserProfileByIdAsync(protector.Unprotect(_cookieSvc.Get("user_id")));
            var addUserModel = new AddUserModel();
            var dashboard = await _dashboardSvc.GetDashboardData();

            _adminBaseViewModel = new AdminBaseViewModel
            {
                Profile = userProfile,
                AddUser = addUserModel,
                AppSetting = _appSettings,
                Dashboard = dashboard,
                SiteWideSetting = _writableSiteWideSettings.Value
            };
            return View("Index", _adminBaseViewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelService;
using UserService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IUserSvc _userSvc;

        public PasswordController(IUserSvc userSvc)
        {
            _userSvc = userSvc;
        }

        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var result = await _userSvc.ResetPassword(model);

                if (result.IsValid)
                {                    
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return BadRequest("Fail");
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolesService;
using Serilog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Controllers.Api.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class UserRoleController : ControllerBase
    {
        private readonly IRoleSvc _roleSvc;

        public UserRoleController(IRoleSvc roleSvc)
        {
            _roleSvc = roleSvc;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles()
        {
            var userRoles = await _roleSvc.GetAllRolesAsync();
            return Ok(userRoles);
        }

        [HttpPost("[action]")]        
        public async Task<IActionResult> AddRole(IFormCollection formData)
        {
            try
            {
                var resultError = await _roleSvc.AddToRolesAsync(formData);

                if (!resultError)
                {
                    // Log.Information("{Info}", "New Role Added.");
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

        [HttpPut("[action]")]        
        public async Task<IActionResult> UpdateRole(IFormCollection formData)
        {
            try
            {
                var resultError = await _roleSvc.UpdateRoleAsync(formData);

                if (!resultError)
                {
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

        [HttpDelete("[action]/{roleId}")]        
        public async Task<IActionResult> DeleteRole([FromRoute] string roleId)
        {
            try
            {
                var resultError = await _roleSvc.DeleteRoleAsync(roleId);

                if (!resultError)
                {
                    Log.Information("{Info}", $"Role with Id {roleId} Deleted.");
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

        [HttpPost("[action]/{rolePermissionName}")]       
        public async Task<IActionResult> AddRolePermission([FromRoute] string rolePermissionName)
        {
            try
            {
                var resultError = await _roleSvc.AddRolePermissionAsync(rolePermissionName);

                if (!resultError)
                {
                    Log.Information("{Info}", $"Role type {rolePermissionName} Added.");
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRolePermissions()
        {
            var roleTypes = await _roleSvc.GetAllRolePermissionsAsync();
            return Ok(roleTypes);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRolePermissionsTypes()
        {
            var roleTypes = await _roleSvc.GetAllRolePermissionsTypesAsync();
            return Ok(roleTypes);
        }

        [HttpDelete("[action]/{rolePermissionId}")]       
        public async Task<IActionResult> DeleteRoleType([FromRoute] string rolePermissionId)
        {
            try
            {
                var rolePermissionToDelete = Convert.ToInt32(rolePermissionId);
                var resultError = await _roleSvc.DeleteRolePermissionAsync(rolePermissionToDelete);

                if (!resultError)
                {
                    Log.Information("{Info}", $"Role type with Id {rolePermissionToDelete} Deleted.");
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
    }
}

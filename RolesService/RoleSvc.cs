using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelService;
using Newtonsoft.Json;
using Serilog;

namespace RolesService
{
    public class RoleSvc : IRoleSvc
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly ApplicationDbContext _db;

        public RoleSvc(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IHostingEnvironment env, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _env = env;
            _db = db;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            try
            {
                var result = await _db.ApplicationRoles.Include(x => x.Permissions).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return null;
        }
        public async Task<bool> AddToRolesAsync(IFormCollection formData)
        {
            var resultError = false;

            try
            {
                ApplicationRole applicationRole = JsonConvert.DeserializeObject<ApplicationRole>(formData["ApplicationRole"]);

                foreach (var permission in applicationRole.Permissions)
                {
                    permission.ApplicationRoleId = applicationRole.Id;
                }

                string iconPath = "";

                if (formData.Files.Count > 0)
                {
                    var extension = ".png";
                    var filename = DateTime.Now.ToString("yymmssfff");
                    iconPath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}roles{Path.DirectorySeparatorChar}icons{Path.DirectorySeparatorChar}", filename) + extension;
                    await using var stream = new FileStream(iconPath, FileMode.Create);
                    await formData.Files[0].CopyToAsync(stream);
                    iconPath = Path.Combine(
                        $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}roles{Path.DirectorySeparatorChar}icons{Path.DirectorySeparatorChar}",
                        filename) + extension;
                }
                else
                {
                    iconPath =
                        $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}roles{Path.DirectorySeparatorChar}icons{Path.DirectorySeparatorChar}default{Path.DirectorySeparatorChar}role.png";
                }

                applicationRole.RoleIcon = iconPath;
                applicationRole.Name = applicationRole.RoleName;
                applicationRole.NormalizedName = applicationRole.RoleName.ToUpper();

                await _db.ApplicationRoles.AddAsync(applicationRole);

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                resultError = true;
            }

            return resultError;
        }
        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var resultError = false;

            try
            {
                var roleToDelete = await _roleManager.FindByIdAsync(roleId);

                if (roleToDelete != null)
                {
                    var rolePermissions = await _db.RolePermissions.Where(x => x.ApplicationRoleId == roleId).ToListAsync();

                    if (rolePermissions != null)
                    {
                        _db.RolePermissions.RemoveRange(rolePermissions);
                    }

                    await _roleManager.DeleteAsync(roleToDelete);

                }
                else
                {
                    resultError = true;
                }


            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                resultError = true;
            }
            return resultError;
        }
        public async Task<bool> UpdateRoleAsync(IFormCollection formData)
        {
            var resultError = false;
            var newPermissions = new List<RolePermission>();

            try
            {
                ApplicationRole applicationRole = JsonConvert.DeserializeObject<ApplicationRole>(formData["ApplicationRole"]);
                string iconPath = "";

                var roleToUpdate = _db.ApplicationRoles.Include(x => x.Permissions).FirstOrDefault(o => o.Id == applicationRole.Id);

                if (roleToUpdate != null)
                {
                    roleToUpdate.Name = applicationRole.RoleName;
                    roleToUpdate.NormalizedName = applicationRole.RoleName.ToUpper();

                    /* Check if new role types exist */
                    var permissionIdList = applicationRole.Permissions.Select(x => x.Id).ToList();


                    foreach (var pid in permissionIdList)
                    {
                        if (roleToUpdate.Permissions.All(x => x.Id != pid))
                        {
                            var entityToAdd = applicationRole.Permissions.FirstOrDefault(x => x.Id == pid);
                            newPermissions.Add(entityToAdd);
                        }
                    }

                    foreach (var permission in newPermissions)
                    {
                        var result = applicationRole.Permissions.Any(x => x.Id == permission.Id);
                        if (result)
                        {
                            var rm = applicationRole.Permissions.FirstOrDefault(x => x.Id == permission.Id);
                            applicationRole.Permissions.Remove(rm);
                        }
                    }

                    roleToUpdate.IsActive = applicationRole.IsActive;
                    roleToUpdate.Handle = applicationRole.Handle;

                    if (formData.Files.Count > 0)
                    {
                        var extension = Path.GetExtension(formData.Files[0].FileName);
                        var filename = DateTime.Now.ToString("yymmssfff");
                        iconPath = Path.Combine(_env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}roles{Path.DirectorySeparatorChar}icons{Path.DirectorySeparatorChar}", filename) + extension;
                        await using var stream = new FileStream(iconPath, FileMode.Create);
                        await formData.Files[0].CopyToAsync(stream);
                        iconPath = Path.Combine(
                            $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}roles{Path.DirectorySeparatorChar}icons{Path.DirectorySeparatorChar}",
                            filename) + extension;
                        roleToUpdate.RoleIcon = iconPath;
                    }



                    _db.Entry(roleToUpdate).State = EntityState.Modified;
                    foreach (var item in newPermissions)
                    {
                        item.Id = 0;
                        item.ApplicationRoleId = roleToUpdate.Id;
                    }
                    await _db.RolePermissions.AddRangeAsync(newPermissions);

                    await _db.SaveChangesAsync();



                }

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                resultError = true;
            }

            return resultError;

        }
        public async Task<bool> AddRolePermissionAsync(string rolePermissionName)
        {
            var resultError = false;

            try
            {
                var pt = new PermissionType { Type = rolePermissionName };
                await _db.PermissionTypes.AddAsync(pt);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                resultError = true;
            }

            return resultError;
        }
        public async Task<IEnumerable<RolePermission>> GetAllRolePermissionsAsync()
        {
            var result = await _db.RolePermissions.ToListAsync();

            return result;
        }
        public async Task<bool> DeleteRolePermissionAsync(int rolePermissionId)
        {
            var resultError = false;

            var roleTypeToDelete = await _db.RolePermissions.FindAsync(rolePermissionId);

            if (roleTypeToDelete != null)
            {
                _db.RolePermissions.Remove(roleTypeToDelete);
            }
            else
            {
                resultError = true;
            }
            return resultError;
        }       
        public async Task<IEnumerable<PermissionType>> GetAllRolePermissionsTypesAsync()
        {
            var result = await _db.PermissionTypes.ToListAsync();

            return result;
        }


    }
}

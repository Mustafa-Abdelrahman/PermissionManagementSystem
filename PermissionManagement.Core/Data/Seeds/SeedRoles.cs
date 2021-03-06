using Microsoft.AspNetCore.Identity;
using PermissionManagement.Web.Data.Constants;

namespace PermissionManagement.Web.Data.Seeds
{
    public static class SeedRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
            }
        }
    }
}

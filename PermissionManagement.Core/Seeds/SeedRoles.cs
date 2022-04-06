using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PermissionManagement.Web.Constants;
using System.Security.Claims;

namespace PermissionManagement.Web.Seeds
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

        //TODO : Add member role default claims
        //public static async Task SeedMemberClaimsAsync(RoleManager<IdentityRole> roleManager, IdentityDbContext dbContext)
        //{
        //    var role = await roleManager.FindByNameAsync(Roles.Member.ToString());
        //    if (role != null)
        //    {
        //        foreach (var view in dbContext.)
        //        {

        //        }
        //    }

        //}
    }
}

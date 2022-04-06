using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PermissionManagement.Web.Constants;
using PermissionManagement.Web.Data;
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
        public static async Task SeedMemberClaimsAsync(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            var role = await roleManager.FindByNameAsync(Roles.Member.ToString());
            var allClaims = await roleManager.GetClaimsAsync(role);
            var pages = Enum.GetValues(typeof(Pages)).Cast<Pages>().ToList();
            var blocks = Enum.GetValues(typeof(Pages)).Cast<Blocks>().ToList();

            if (role != null)
            {
                foreach (var page in pages)
                {
                    string pageName = page.ToString();
                    if (!allClaims.Any(c => c.Type == "View" && c.Value == pageName) && page != Pages.PermissionsManagement)
                    {
                        await roleManager.AddClaimAsync(role, new Claim("View", pageName));
                    }
                }
                foreach (var block in blocks)
                {
                    var blockName = block.ToString();
                    if (!allClaims.Any(c => c.Type == "View" && c.Value == blockName))
                    {
                        await roleManager.AddClaimAsync(role, new Claim("View", blockName));
                    }
                }
            }
        }
        public static async Task SeedAdminClaimsAsync(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            var role = await roleManager.FindByNameAsync(Roles.Administrator.ToString());
            var allClaims = await roleManager.GetClaimsAsync(role);

            if (role != null && !allClaims.Any(c => c.Type == "View" && c.Value == Pages.PermissionsManagement.ToString()))
                await roleManager.AddClaimAsync(role, new Claim("View", Pages.PermissionsManagement.ToString()));
        }
    }
}

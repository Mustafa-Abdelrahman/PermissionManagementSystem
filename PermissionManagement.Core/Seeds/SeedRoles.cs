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
            var pages = dbContext.Pages.ToList();
            var blocks = dbContext.Blocks.ToList();

            if (role != null)
            {
                for (int i = 0; i < pages.Count; i++)
                {
                    string pageName = pages[i].Name;
                    if (!allClaims.Any(c => c.Type == "View" && c.Value == pageName) && pageName != "Permissions Management")
                    {
                        await roleManager.AddClaimAsync(role, new Claim("View", pageName));

                    }
                }

                for (int i = 0; i < blocks.Count; i++)
                {
                    var blockName = blocks[i].Name;
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

            if (role != null && !allClaims.Any(c => c.Type == "View" && c.Value == "Permissions Management"))
                await roleManager.AddClaimAsync(role, new Claim("View", dbContext.Pages.First(p => p.Name == "Permissions Management").Name));
        }
    }
}

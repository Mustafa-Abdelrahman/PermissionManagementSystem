using Microsoft.AspNetCore.Identity;
using PermissionManagement.Web.Data.Constants;

namespace PermissionManagement.Web.Data.Seeds
{
    public static class SeedUsers
    {

        public static async Task SeedAdminAsync(UserManager<IdentityUser> userManager)
        {
            var AdminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@domain.com",
                EmailConfirmed = true
            };
            var User = await userManager.FindByEmailAsync(AdminUser.Email);

            if (User == null)
            {
                await userManager.CreateAsync(AdminUser, "P@ssw0rd");
                await userManager.AddToRoleAsync(AdminUser, Roles.Administrator.ToString());
            }
        }

        public static async Task SeedMemberAsync(UserManager<IdentityUser> userManager)
        {

            for (int i = 1; i <= 3; i++)
            {
                var member = new IdentityUser
                {
                    UserName = $"member{i}",
                    Email = $"member{i}@domain.com",
                    EmailConfirmed = true
                };
                var user = await userManager.FindByEmailAsync(member.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(member, "P@ssw0rd");
                    await userManager.AddToRoleAsync(member, Roles.Member.ToString());
                }
            }
           
        }
    }
}

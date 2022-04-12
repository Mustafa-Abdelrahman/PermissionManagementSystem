using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.Web.Business.Contracts;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Data.Constants;
using PermissionManagement.Web.Data.ViewModels;
using System.Security.Claims;

namespace PermissionManagement.Web.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly HttpContext httpContext;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserService(UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            ApplicationDbContext dbContext,
                            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
            this.httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<IdentityUser> GetLoggedInUserAsync()
        {
            if (httpContext is not null)
                return await userManager.GetUserAsync(httpContext.User);

            return null;

        }

        public async Task<List<string>> GetUserClaimsStringValuesAsync(string id = "")
        {
            if (string.IsNullOrEmpty(id))
                   return await dbContext.UserClaims.Where(c => c.UserId == GetLoggedInUserAsync().Result.Id).Select(claim => claim.ClaimValue).ToListAsync();

            return await dbContext.UserClaims.Where(c => c.UserId == id).Select(claim => claim.ClaimValue).ToListAsync();
        }

        public async Task<IdentityRole> GetUserRoleAsync(string Id)
        {
            var userRoleId =  dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == Id)?.Result?.RoleId;

            var userRole = roleManager.Roles.Where(r => r.Id == userRoleId).FirstOrDefaultAsync().Result; //Get User role
            return userRole;
        }

        public async Task SaveClaimsAsync(UserAuthorizationVM editedPermissionsVM)
        {
            var user = await userManager.FindByIdAsync(editedPermissionsVM.UserId);

            if (user is not null)
            {
                // Delete all existing claims
                await this.DeleteClaimsAsync(user, userManager.GetClaimsAsync(user).Result.ToList());

                var selectedClaims = editedPermissionsVM.UserSelectedClaims.Where(x => x.IsSelected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                }
                return;
            }
            throw new NullReferenceException(nameof(user));
        }

        private async Task DeleteClaimsAsync(IdentityUser user, List<Claim> claims)
        {
            await userManager.RemoveClaimsAsync(user, claims);
        }

        public async Task<UserAuthorizationVM> GetUserAuthorizationVM(string Id)
        {
           
            var viewModel = new UserAuthorizationVM
            {
                UserId = Id
            };
            var userRole =  await GetUserRoleAsync(Id);

            if (userRole != null)
            {
                var membersAreaPages = await dbContext.Pages.Where(p => p.AssociatedRole == Roles.Member.ToString()).Include(pb => pb.PageBlocks).ToListAsync() ; // GetMember role pages

                var userClaims = await this.GetUserClaimsStringValuesAsync(Id); // userclaims

                foreach (var page in membersAreaPages)
                {
                    viewModel.UserSelectedClaims.Add(new ClaimsVM { Value = page.Name, Type = ClaimTypes.Webpage, IsSelected = userClaims.Contains(page.Name) });

                    foreach (var block in page.PageBlocks)
                    {
                        viewModel.UserSelectedClaims.Add(new ClaimsVM { Value = block.Name, Type = "Block", IsSelected = userClaims.Contains(block.Name) });
                    }
                }
                return viewModel; 
            }
            throw new NullReferenceException(nameof(userRole));
        }

    }
}

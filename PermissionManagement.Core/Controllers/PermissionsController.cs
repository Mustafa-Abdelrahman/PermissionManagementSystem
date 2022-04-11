using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.ViewModels;
using System.Security.Claims;

namespace PermissionManagement.Web.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PermissionsController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index(string userId)
        {
            var userRoleId = _dbContext.UserRoles.FirstOrDefault(u => u.UserId == userId)?.RoleId;

            var userRole = _roleManager.Roles.Where(r => r.Id == userRoleId).FirstOrDefault();

            var viewModel = new EditPermissionsVM
            {
                UserId = userId,
            };

            if (userRole != null)
            {
                var roleClaims = _roleManager.GetClaimsAsync(userRole).Result.ToList();

                var userClaims = _dbContext.UserClaims.Where(uc => uc.UserId == userId).Select(x => x.ClaimValue).ToList();


                roleClaims.ForEach(roleClaim => viewModel.UserSelectedClaims
                               .Add(new PermissionVM { Value = roleClaim.Value, Type = roleClaim.Type, IsSelected = userClaims.Contains(roleClaim.Value) }));
            }
            return PartialView("_PermissionsFormPartial", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SavePermissions(EditPermissionsVM editPermissionsVM)
        {
            if (editPermissionsVM.UserId != null)
            {
                //var userClaims = _dbContext.UserClaims.Where(x => x.UserId == editPermissionsVM.UserId).ToList();
                //_dbContext.UserClaims.RemoveRange(userClaims);
                var user =  await _userManager.FindByIdAsync(editPermissionsVM.UserId);
                var identityClaims = await _userManager.GetClaimsAsync(user);

                if (user != null)
                {
                   await _userManager.RemoveClaimsAsync(user, identityClaims);
                }

                var selectedClaims = editPermissionsVM.UserSelectedClaims.Where(x => x.IsSelected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                    //_dbContext.Add(new IdentityUserClaim<string> { UserId = editPermissionsVM.UserId, ClaimType = claim.Type, ClaimValue = claim.Value });
                }


                //_dbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Permissions");
        }
    }
}

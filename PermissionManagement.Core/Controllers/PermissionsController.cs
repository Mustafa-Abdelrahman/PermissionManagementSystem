using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


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
                var rolePages = _dbContext.Pages.Where(p => p.AssociatedRole == userRole.Name).Include(pb => pb.PageBlocks).ToList();
                
                var userClaims = _dbContext.UserClaims.Where(uc => uc.UserId == userId).Select(x => x.ClaimValue).ToList();

                foreach (var page in rolePages)
                {
                    viewModel.UserSelectedClaims.Add(new PermissionVM { Value = page.Name, Type = ClaimTypes.Webpage, IsSelected = userClaims.Contains(page.Name) });

                    foreach (var block in page.PageBlocks)
                    {
                        viewModel.UserSelectedClaims.Add(new PermissionVM { Value = block.Name, Type = "Block", IsSelected = userClaims.Contains(block.Name) });
                    }
                }
            }
            return PartialView("_PermissionsFormPartial", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SavePermissions(EditPermissionsVM editPermissionsVM)
        {
            if (editPermissionsVM.UserId != null)
            {
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
                }

            }
            return RedirectToAction("Index", "Admin");
        }
    }
}

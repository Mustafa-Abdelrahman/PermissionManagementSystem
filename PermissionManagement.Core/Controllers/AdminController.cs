using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Constants;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Security.Filters;
using PermissionManagement.Web.ViewModels;
using System.Security.Claims;

namespace PermissionManagement.Web.Controllers
{
    //[AuthorizedRole(new Roles[] { Roles.Administrator })]
    [Authorize(Policy = "Admins Only")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userRole = _dbContext.UserRoles.FirstOrDefault(ur => ur.UserId == currentUser.Id);
            var allUsers = _dbContext.Users.Where(u => u.Id != currentUser.Id).OrderBy(x => x.UserName).ToList();
            UsersPermissionsVM viewModel = new UsersPermissionsVM { AllUsers = allUsers , SelectedUserId = string.Empty};
            return View(viewModel);
        }
    }
}

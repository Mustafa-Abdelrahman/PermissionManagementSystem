using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Data.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.Web.Business.Contracts;

namespace PermissionManagement.Web.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IUserService userService;

        public PermissionsController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> Index(string userId)
        {
            var viewModel = await userService.GetUserAuthorizationVM(userId);
            return PartialView("_PermissionsFormPartial", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SavePermissions(UserAuthorizationVM editPermissionsVM)
        {
            await userService.SaveClaimsAsync(editPermissionsVM);
            return RedirectToAction("Index", "Admin");
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Data.Constants;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Security.Filters;
using PermissionManagement.Web.Data.ViewModels;
using System.Security.Claims;
using PermissionManagement.Web.Business.Contracts;

namespace PermissionManagement.Web.Controllers
{
    //[AuthorizedRole(new Roles[] { Roles.Administrator })]
    [Authorize(Policy = "Admins Only")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IMembersService membersService;

        public AdminController(IUserService userService, IMembersService membersService)
        {
            this.userService = userService;
            this.membersService = membersService;
        }
        public async Task<IActionResult> Index()
        {
            MembersListVM viewModel = await membersService.GetMembersPermissionsVMAsync();
            return View(viewModel);
        }
    }
}

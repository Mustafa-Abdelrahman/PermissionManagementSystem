using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Data.Constants;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Security.Filters;
using System.Security.Claims;
using PermissionManagement.Web.Business.Contracts;

namespace PermissionManagement.Web.Controllers
{
    [Authorize(Policy = "Members Only")]
    public class MembersController : Controller
    {
        private readonly IUserService userService;

        public MembersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "Access Page1")]
        public async Task<IActionResult> Page1()
        {   
            var userclaims = await userService.GetUserClaimsStringValuesAsync();
            return View(userclaims);
        }

        [Authorize(Policy = "Access Page2")]
        public async Task<IActionResult> Page2()
        {
            var userclaims = await userService.GetUserClaimsStringValuesAsync();
            return View(userclaims);
        }

    }
}

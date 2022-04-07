using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Constants;
using PermissionManagement.Web.Security.Filters;

namespace PermissionManagement.Web.Controllers
{
    [AuthorizedRole(Roles = new Roles[] { Roles.Administrator })]
    public class Permissions : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

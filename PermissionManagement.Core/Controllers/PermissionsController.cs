using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Constants;
using PermissionManagement.Web.Security.Filters;

namespace PermissionManagement.Web.Controllers
{
    [AuthorizedRole(new Roles[] { Roles.Administrator })]
    public class PermissionsController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}

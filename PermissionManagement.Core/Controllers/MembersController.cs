using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.Web.Constants;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Security.Filters;
using System.Security.Claims;

namespace PermissionManagement.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        public List<string> userClaimsValues { get; set; }
        private ApplicationDbContext _dbContext;
           
        public MembersController(ApplicationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        [Authorize(Policy = "Page1Access")]
        public IActionResult Page1()
        {
            var claims = User.Identities.First().Claims.ToList();

            GetClaims();
            return View(userClaimsValues);
        }

        [Authorize(Policy = "Page2Access")]
        public IActionResult Page2()
        {
            GetClaims();
            return View(userClaimsValues);
        }

        private void GetClaims()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userClaimsValues = _dbContext.UserClaims.Where(c=> c.UserId == userId).Select(claim => claim.ClaimValue).ToList();
        }
    }
}

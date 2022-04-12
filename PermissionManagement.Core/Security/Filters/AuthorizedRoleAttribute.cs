using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PermissionManagement.Web.Data.Constants;

namespace PermissionManagement.Web.Security.Filters
{
    public class AuthorizedRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public Roles[] Roles { get; set; }
        public AuthorizedRoleAttribute(params Roles[] roles)
        {
            this.Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user != null)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (!user.Identity.IsAuthenticated)
                { 
                    context.Result = new UnauthorizedResult();
                }

                if (!Roles.Any(role => user.IsInRole(role.ToString())))
                {
                    context.Result = new UnauthorizedResult();
                }

            }
        }
    }
}

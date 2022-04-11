using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PermissionManagement.Web.ViewModels
{
    public class UsersPermissionsVM
    {
        public UsersPermissionsVM()
        {
            AllUsers = new List<IdentityUser>();
        }
        public List<IdentityUser> AllUsers { get; set; }
        public string SelectedUserId { get; set; }
    }
}

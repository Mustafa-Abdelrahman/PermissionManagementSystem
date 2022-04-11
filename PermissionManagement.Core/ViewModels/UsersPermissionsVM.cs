using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PermissionManagement.Web.ViewModels
{
    public class UsersPermissionsVM
    {
        public UsersPermissionsVM()
        {
            //UserSelectedClaims = new List<PermissionVM>();
            //AllUserRoleClaims = new List<string>();
            AllUsers = new List<IdentityUser>();
        }
        public List<IdentityUser> AllUsers { get; set; }
        public string SelectedUserId { get; set; }
        //public string SelectedUserName { get; set; }
        //public List<PermissionVM> UserSelectedClaims { get; set; }
        //public List<string> AllUserRoleClaims { get; set; }
    }
}

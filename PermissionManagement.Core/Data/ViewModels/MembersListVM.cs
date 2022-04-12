using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PermissionManagement.Web.Data.ViewModels
{
    public class MembersListVM
    {
        public MembersListVM()
        {
            AllMembers = new List<IdentityUser>();
        }
        public List<IdentityUser> AllMembers { get; set; }
        public string SelectedUserId { get; set; }
    }
}

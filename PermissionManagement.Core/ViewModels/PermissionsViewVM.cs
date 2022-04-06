using Microsoft.AspNetCore.Identity;

namespace PermissionManagement.Web.ViewModels
{
    public class PermissionsViewVM
    {
        public List<IdentityUser> Users { get; set; }
        public List<PermissionVM> Permissions { get; set; }
    }
}

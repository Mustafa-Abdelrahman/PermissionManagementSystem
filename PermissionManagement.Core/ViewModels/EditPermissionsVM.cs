using PermissionManagement.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.ViewModels
{
    public class EditPermissionsVM
    {
        public EditPermissionsVM()
        {
            UserSelectedClaims = new List<PermissionVM>();
        }

        [Key]
        public string UserId { get; set; }
        public List<PermissionVM> UserSelectedClaims { get; set; }
    }
}

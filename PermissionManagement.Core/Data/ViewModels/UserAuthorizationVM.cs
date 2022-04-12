using PermissionManagement.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.Data.ViewModels
{
    public class UserAuthorizationVM
    {
        public UserAuthorizationVM()
        {
            UserSelectedClaims = new List<ClaimsVM>();
        }

        [Key]
        public string UserId { get; set; }
        public List<ClaimsVM> UserSelectedClaims { get; set; }
    }
}

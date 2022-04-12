using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.Data.ViewModels
{
    public class ClaimsVM
    {
        [Key]
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsSelected { get; set; }
    }
}

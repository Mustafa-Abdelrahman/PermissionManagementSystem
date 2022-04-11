using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.ViewModels
{
    public class PermissionVM
    {
        [Key]
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsSelected { get; set; }
    }
}

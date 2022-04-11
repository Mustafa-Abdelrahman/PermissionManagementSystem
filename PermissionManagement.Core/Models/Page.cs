using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssociatedRole { get; set; }
        public ICollection<Block> PageBlocks { get; set; }
    }
}

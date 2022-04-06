using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public Page Page { get; set; }
    }
}

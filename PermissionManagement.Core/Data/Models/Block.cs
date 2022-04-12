using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionManagement.Web.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int PageId { get; set; }
    }
}

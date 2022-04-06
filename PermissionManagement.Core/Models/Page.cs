using System.ComponentModel.DataAnnotations;

namespace PermissionManagement.Web.Models
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Block> PageBlocks { get; set; }
    }
}

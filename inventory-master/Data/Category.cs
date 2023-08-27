using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Data
{
    [Table("Categorys")]
    public class Category
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public StatusEnum Status { set; get; }
        public List<Product> Products { set; get; }
    }
}

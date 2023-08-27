using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Data
{
    [Table("Suppliers")]
    public class Supplier
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string phone { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public string ContactPerson { set; get; }
        [Required]
        public string ContactPersonPhone { set; get; }
        public List<Product> Products { set; get; }

    }
}

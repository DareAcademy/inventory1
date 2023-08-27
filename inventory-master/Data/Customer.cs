using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Data
{
    [Table("Customers")]
    public class Customer
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
        public TypeEnum Type { set; get; }
        public List<Orders> orders { set; get; }

    }
}

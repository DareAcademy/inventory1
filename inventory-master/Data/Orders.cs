using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Data
{
    [Table("Orders")]
    public class Orders
    {
        public int Id { set; get; }
        [Required]
        public int Qty { set; get; }
        [Required]
        public DateTime OrderDate { set; get; }
        [Required]
        public Double Price { set; get; }
        [Required]
        public Double Discount { set; get; }
        [Required]
        public PaymentMethodType PaymentMethod { set; get; }

        [ForeignKey("Product")]
        public int Product_Id { set; get; }
        public Product Product { set; get; }

        [ForeignKey("Customer")]
        public int Customer_Id { set; get; }
        public Customer Customer { set; get; }
    }
}

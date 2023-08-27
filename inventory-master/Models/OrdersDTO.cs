using InventorySystem.Data.Enum;
using InventorySystem.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace InventorySystem.Models
{
    [AutoMap(typeof(Orders), ReverseMap = true)]
    public class OrdersDTO
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
        public int Product_Id { set; get; }
        public ProductDTO? Product { set; get; }
        public int Customer_Id { set; get; }
        public CustomerDTO? Customer { set; get; }

    }
}

using InventorySystem.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace InventorySystem.Models
{
    [AutoMap(typeof(Product), ReverseMap = true)]
    public class ProductDTO
    {
        public int Id { set; get; }
        [Required]
        public String Name { set; get; }
        [Required]
        public Double SellPrice { set; get; }
        [Required]
        public Double BuyingPrice { set; get; }
        [Required]
        public Double Qty { set; get; }
        public string? Description { set; get; }
        public string? path { set; get; }
        public IFormFile image { set; get; }
        public Boolean IsAvailable { set; get; }
        public DateTime AddedDate { set; get; }
        public int Category_Id { set; get; }
        public CategoryDTO? Category { set; get; }  
        public int Supplier_Id { set; get; }
        public SupplierDTO? Supplier { set; get; }
    }
}

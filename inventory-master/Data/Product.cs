using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Data
{
    [Table("Products")]
    public class Product
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
        [NotMapped]
        public IFormFile image { set; get; }
        public Boolean IsAvailable { set; get; }
        public DateTime AddedDate { set; get; }
        public List<Orders> orders { set; get; }

        [ForeignKey("Category")]
        public int Category_Id { set; get; }
        public Category Category { set; get; }

        [ForeignKey("Supplier")]
        public int Supplier_Id { set; get; }
        public Supplier Supplier { set; get; }
    }
}

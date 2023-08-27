using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Data
{
    [Table("Companies")]
    public class Company
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string phone { set; get; }
        
        public string Address { set; get; }
        
        public Double TaxPersent { set; get; }
        public List<Stores> stores { set; get; }
    }
}

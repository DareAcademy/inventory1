using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace InventorySystem.Data
{

    [Table("Stores")]
    public class Stores
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string phone { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        public StatusEnum Status { set; get; }

        [ForeignKey("Company")]
        public int Company_Id { set; get; }
        public Company Company { set; get; }

    }
}

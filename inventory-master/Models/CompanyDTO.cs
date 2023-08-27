using AutoMapper;
using InventorySystem.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace InventorySystem.Models
{
    [AutoMap(typeof(Company), ReverseMap = true)]
    public class CompanyDTO
    {
        public int? Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string phone { set; get; }
        
        public string Address { set; get; }
        
        public Double TaxPersent { set; get; }
    }
}

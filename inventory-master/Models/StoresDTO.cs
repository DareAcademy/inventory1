using InventorySystem.Data.Enum;
using InventorySystem.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace InventorySystem.Models
{
    [AutoMap(typeof(Stores), ReverseMap = true)]
    public class StoresDTO
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
        public int Company_Id { set; get; }
        public CompanyDTO? Company { set; get; }
    }
}

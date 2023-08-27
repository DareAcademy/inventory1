using AutoMapper;
using InventorySystem.Data;
using InventorySystem.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    [AutoMap(typeof(Category), ReverseMap = true)]
    public class CategoryDTO
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public StatusEnum Status { set; get; }
    }
}

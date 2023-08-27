using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}

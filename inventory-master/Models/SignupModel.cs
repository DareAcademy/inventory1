using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class SignupModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [RegularExpression(@"07(7|8|9)\d{7}")]
        public string Phone { get; set; }

        public string? ProfileImage { set; get; }
        public IFormFile image { set; get; }

    }
}

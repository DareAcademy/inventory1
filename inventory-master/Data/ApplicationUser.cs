using Microsoft.AspNetCore.Identity;

namespace InventorySystem.data
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string? ProfileImage { get; set; }
    }
}

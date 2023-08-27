using InventorySystem.Data;

namespace InventorySystem.Models
{
    public class vmProduct
    {
        public ProductDTO product { set; get; }
        public List<Category> LiCategory { set; get; }
        public List<Supplier> LiSupplier { set; get; }
    }
}

using InventorySystem.Data;

namespace InventorySystem.Models
{
    public class vmOrders
    {
        public OrdersDTO Order { set; get; }
        public List<ProductDTO> LiProduct { set; get; }
        public List<CustomerDTO> LiCustomer { set; get; }
    }
}

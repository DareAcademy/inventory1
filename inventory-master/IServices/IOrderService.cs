using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface IOrderService
    {
        void Create(OrdersDTO ordersDTO);
        void Update(OrdersDTO ordersDTO);
        PaginatedList<OrdersDTO> GetAll(int CurrentPage);
        OrdersDTO Get(int Id);
        List<Orders> GetOrder();
        void Delete(int Id);
        PaginatedList<OrdersDTO> loadbyname(string name, int CurrentPage);
        List<OrdersDTO> GetAll();
    }
}

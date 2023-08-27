using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface ICustomerService
    {
        void Create(CustomerDTO customerDTO);
        void Update(CustomerDTO customerDTO);
        PaginatedList<CustomerDTO> GetAll(int CurrentPage);
        CustomerDTO Get(int Id);
        void Delete(int Id);
        List<CustomerDTO> GetCustomer();
        PaginatedList<CustomerDTO> loadbyname(string name, int CurrentPage);

        List<CustomerDTO> GetAll();
    }
}

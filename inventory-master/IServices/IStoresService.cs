using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface IStoresService
    {
        void Create(StoresDTO storesDTO);
        void Update(StoresDTO storesDTO);
        PaginatedList<StoresDTO> GetAll(int CurrentPage);
        StoresDTO Get(int Id);
        void Delete(int Id);
        List<Stores> GetStore();
        PaginatedList<StoresDTO> loadbyname(string name, int CurrentPage);

        List<StoresDTO> GetAll();
    }
}

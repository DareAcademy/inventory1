using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface ISupplierService
    {

        void Create(SupplierDTO supplierDTO);
        void Update(SupplierDTO supplierDTO);
        PaginatedList<SupplierDTO> GetAll(int CurrentPage);
        SupplierDTO Get(int Id);
        void Delete(int Id);
        List<Supplier> GetSupplier();
        PaginatedList<SupplierDTO> loadbyname(string name, int CurrentPage);

        List<SupplierDTO> GetAll();
    }
}

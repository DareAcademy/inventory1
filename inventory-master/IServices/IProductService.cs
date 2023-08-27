using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface IProductService
    {
        void Create(ProductDTO productDTO);
        void Update(ProductDTO productDTO);
        PaginatedList<ProductDTO> GetAll(int CurrentPage);
        ProductDTO Get(int Id);
        void Delete(int Id);
        List<ProductDTO> GetProduct();
        PaginatedList<ProductDTO> loadbyname(string name, int CurrentPage);

        List<ProductDTO> GetAll();
    }
}

using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface ICategoryService
    {
        void Create(CategoryDTO categoryDTO);
        void Update(CategoryDTO categoryDTO);
        PaginatedList<CategoryDTO> GetAll(int CurrentPage);
        CategoryDTO Get(int Id);
        void Delete(int Id);
        List<Category> GetCategory();
        PaginatedList<CategoryDTO> loadbyname(string name, int CurrentPage);


    }
}

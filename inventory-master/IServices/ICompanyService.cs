using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.Models;

namespace InventorySystem.IServices
{
    public interface ICompanyService
    {
        void Create(CompanyDTO companyDTO);
        void Update(CompanyDTO companyDTO);
        PaginatedList<CompanyDTO> GetAll(int CurrentPage);
        CompanyDTO Get(int Id);
        void Delete(int Id);
        List<Company> GetCompany();
        PaginatedList<CompanyDTO> loadbyname(string name, int CurrentPage);
    }
}

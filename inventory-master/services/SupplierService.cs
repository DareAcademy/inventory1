using AutoMapper;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class SupplierService : ISupplierService
    {
        private readonly InventoryContext context;
        private readonly IMapper mapper;

        public SupplierService(InventoryContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Create(SupplierDTO supplierDTO)
        {
            Supplier newSupplier = mapper.Map<Supplier>(supplierDTO);

            context.Suppliers.Add(newSupplier);
            context.SaveChanges();
        }

        public void Update(SupplierDTO supplierDTO)
        {
            Supplier newSupplier = mapper.Map<Supplier>(supplierDTO);

            context.Suppliers.Attach(newSupplier);
            context.Entry(newSupplier).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PaginatedList<SupplierDTO> GetAll(int CurrentPage)
        {
            List<Supplier> allSupplier = context.Suppliers.ToList();
            List<SupplierDTO> Suppliers = mapper.Map<List<SupplierDTO>>(allSupplier);

            PaginatedList<SupplierDTO> PaginatSuppliers = PaginatedList<SupplierDTO>.CreateAsync(Suppliers, CurrentPage);

            return PaginatSuppliers;
        }

        public List<SupplierDTO> GetAll()
        {
            List<Supplier> allSupplier = context.Suppliers.ToList();
            List<SupplierDTO> Suppliers = mapper.Map<List<SupplierDTO>>(allSupplier);

            return Suppliers;
        }

        public SupplierDTO Get(int Id)
        {
            Supplier supplier = context.Suppliers.Find(Id);
            SupplierDTO supplierDTO = mapper.Map<SupplierDTO>(supplier);

            return supplierDTO;
        }

        public void Delete(int Id)
        {
            Supplier supplier = context.Suppliers.Find(Id);

            context.Suppliers.Remove(supplier);
            context.SaveChanges();
        }
        public List<Supplier> GetSupplier()
        {
            List<Supplier> li = context.Suppliers.ToList();

            return li;
        }
        public PaginatedList<SupplierDTO> loadbyname(string name, int CurrentPage)
        {
            List<Supplier> li = context.Suppliers.Where(p => p.Name.Contains(name)).ToList();

            List<SupplierDTO> Suppliers = mapper.Map<List<SupplierDTO>>(li);

            PaginatedList<SupplierDTO> PaginatSuppliers = PaginatedList<SupplierDTO>.CreateAsync(Suppliers, CurrentPage);

            return PaginatSuppliers;
        }
    }
}

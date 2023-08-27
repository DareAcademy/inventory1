using AutoMapper;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class StoresService : IStoresService
    {
        private readonly InventoryContext context;
        private readonly IMapper mapper;

        public StoresService(InventoryContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Create(StoresDTO storesDTO)
        {
            Stores newStore = mapper.Map<Stores>(storesDTO);

            context.Stores.Add(newStore);
            context.SaveChanges();
        }

        public void Update(StoresDTO storesDTO)
        {
            Stores newStore = mapper.Map<Stores>(storesDTO);

            context.Stores.Attach(newStore);
            context.Entry(newStore).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PaginatedList<StoresDTO> GetAll(int CurrentPage)
        {
            List<Stores> allStore = context.Stores.Include("Company").ToList();
            List<StoresDTO> stores = mapper.Map<List<StoresDTO>>(allStore);

            PaginatedList<StoresDTO> PaginatStores = PaginatedList<StoresDTO>.CreateAsync(stores, CurrentPage);

            return PaginatStores;
        }

        public List<StoresDTO> GetAll()
        {
            List<Stores> allStore = context.Stores.Include("Company").ToList();
            List<StoresDTO> stores = mapper.Map<List<StoresDTO>>(allStore);

            return stores;
        }

        public StoresDTO Get(int Id)
        {
            Stores stores = context.Stores.Find(Id);
            StoresDTO storesDTO = mapper.Map<StoresDTO>(stores);

            return storesDTO;
        }

        public void Delete(int Id)
        {
            Stores stores = context.Stores.Find(Id);

            context.Stores.Remove(stores);
            context.SaveChanges();
        }
        public List<Stores> GetStore()
        {
            List<Stores> li = context.Stores.Include("Company").ToList();


            return li;
        }
        public PaginatedList<StoresDTO> loadbyname(string name, int CurrentPage)
        {
            List<Stores> li = context.Stores.Include("Company").Where(p => p.Name.Contains(name)).ToList();

            List<StoresDTO> Stores = mapper.Map<List<StoresDTO>>(li);

            PaginatedList<StoresDTO> PaginatStores = PaginatedList<StoresDTO>.CreateAsync(Stores, CurrentPage);

            return PaginatStores;
        }
    }
}

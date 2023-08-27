using AutoMapper;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class CategoryService :ICategoryService
    {
        private readonly InventoryContext context;
        private readonly IMapper mapper;

        public CategoryService(InventoryContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Create(CategoryDTO categoryDTO)
        {
            Category newCategory = mapper.Map<Category>(categoryDTO);

            context.Categorys.Add(newCategory);
            context.SaveChanges();
        }

        public void Update(CategoryDTO categoryDTO)
        {
            Category newCategory = mapper.Map<Category>(categoryDTO);

            context.Categorys.Attach(newCategory);
            context.Entry(newCategory).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PaginatedList<CategoryDTO> GetAll(int CurrentPage)
        {
            List<Category> allCategory = context.Categorys.ToList();
            List<CategoryDTO> Categorys = mapper.Map<List<CategoryDTO>>(allCategory);

            PaginatedList<CategoryDTO> PaginatCategorys = PaginatedList<CategoryDTO>.CreateAsync(Categorys, CurrentPage);

            return PaginatCategorys;
        }



        public CategoryDTO Get(int Id)
        {
            Category category = context.Categorys.Find(Id);
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public void Delete(int Id)
        {
            Category category = context.Categorys.Find(Id);

            context.Categorys.Remove(category);
            context.SaveChanges();
        }
        public List<Category> GetCategory()
        {
            List<Category> li = context.Categorys.ToList();
            return li;
        }

        public PaginatedList<CategoryDTO> loadbyname(string name, int CurrentPage)
        {
            List<Category> li = context.Categorys.Where(p => p.Name.Contains(name)).ToList();

            List<CategoryDTO> Categorys = mapper.Map<List<CategoryDTO>>(li);

            PaginatedList<CategoryDTO> PaginatCategorys = PaginatedList<CategoryDTO>.CreateAsync(Categorys, CurrentPage);

            return PaginatCategorys;
        }
    }
}

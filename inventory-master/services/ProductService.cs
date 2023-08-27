using AutoMapper;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class ProductService :IProductService
    {
        private readonly InventoryContext context;
        private readonly IMapper mapper;

        public ProductService(InventoryContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Create(ProductDTO productDTO)
        {
            Product newProduct = mapper.Map<Product>(productDTO);

            context.Products.Add(newProduct);
            context.SaveChanges();
        }

        public void Update(ProductDTO productDTO)
        {
            Product newProduct = mapper.Map<Product>(productDTO);

            context.Products.Attach(newProduct);
            context.Entry(newProduct).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PaginatedList<ProductDTO> GetAll(int CurrentPage)
        {
            List<Product> allProduct = context.Products.Include(o => o.Category).Include(o => o.Supplier).ToList();
            List<ProductDTO> products = mapper.Map<List<ProductDTO>>(allProduct);

            PaginatedList<ProductDTO> PaginatProducts = PaginatedList<ProductDTO>.CreateAsync(products, CurrentPage);

            return PaginatProducts;
        }

        public List<ProductDTO> GetAll()
        {
            List<Product> allProduct = context.Products.Include(o => o.Category).Include(o => o.Supplier).ToList();
            List<ProductDTO> products = mapper.Map<List<ProductDTO>>(allProduct);

            return products;
        }

        public ProductDTO Get(int Id)
        {
            Product product = context.Products.Find(Id);
            ProductDTO productDTO = mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public void Delete(int Id)
        {
            Product product = context.Products.Find(Id);

            context.Products.Remove(product);
            context.SaveChanges();
        }
        public List<ProductDTO> GetProduct()
        {
            List<Product> li = context.Products.Include(o => o.Category).Include(o => o.Supplier).ToList();
            List<ProductDTO> Products = mapper.Map<List<ProductDTO>>(li);
            return Products;
        }
        public PaginatedList<ProductDTO> loadbyname(string name, int CurrentPage)
        {
            List<Product> li = context.Products.Include(o => o.Category).Include(o => o.Supplier).Where(p => p.Name.Contains(name)).ToList();

            List<ProductDTO> Products = mapper.Map<List<ProductDTO>>(li);

            PaginatedList<ProductDTO> PaginatProducts = PaginatedList<ProductDTO>.CreateAsync(Products, CurrentPage);

            return PaginatProducts;
        }
    }
}

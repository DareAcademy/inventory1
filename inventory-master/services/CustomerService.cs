using AutoMapper;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class CustomerService :ICustomerService
    {
        private readonly InventoryContext context;
        private readonly IMapper mapper;

        public CustomerService(InventoryContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Create(CustomerDTO customerDTO)
        {
            Customer newCustomer = mapper.Map<Customer>(customerDTO);

            context.Customers.Add(newCustomer);
            context.SaveChanges();
        }

        public void Update(CustomerDTO customerDTO)
        {
            Customer newCustomer = mapper.Map<Customer>(customerDTO);

            context.Customers.Attach(newCustomer);
            context.Entry(newCustomer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PaginatedList<CustomerDTO> GetAll(int CurrentPage)
        {
            List<Customer> allcustomer = context.Customers.ToList();
            List<CustomerDTO> customeres = mapper.Map<List<CustomerDTO>>(allcustomer);

            PaginatedList<CustomerDTO> Paginatcustomeres = PaginatedList<CustomerDTO>.CreateAsync(customeres, CurrentPage);

            return Paginatcustomeres;
        }

        public List<CustomerDTO> GetAll()
        {
            List<Customer> allcustomer = context.Customers.ToList();
            List<CustomerDTO> customeres = mapper.Map<List<CustomerDTO>>(allcustomer);

            return customeres;
        }

        public CustomerDTO Get(int Id)
        {
            Customer customer = context.Customers.Find(Id);
            CustomerDTO customerDTO = mapper.Map<CustomerDTO>(customer);

            return customerDTO;
        }

        public void Delete(int Id)
        {
            Customer customer = context.Customers.Find(Id);

            context.Customers.Remove(customer);
            context.SaveChanges();
        }
        public List<CustomerDTO> GetCustomer()
        {
            List<Customer> li = context.Customers.ToList();
            List<CustomerDTO> Customers = mapper.Map<List<CustomerDTO>>(li);

            return Customers;
        }
        public PaginatedList<CustomerDTO> loadbyname(string name, int CurrentPage)
        {
            List<Customer> li = context.Customers.Where(p => p.Name.Contains(name)).ToList();

            List<CustomerDTO> Customers = mapper.Map<List<CustomerDTO>>(li);

            PaginatedList<CustomerDTO> PaginatCustomers = PaginatedList<CustomerDTO>.CreateAsync(Customers, CurrentPage);

            return PaginatCustomers;
        }
        
    }
}

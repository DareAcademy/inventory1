using AutoMapper;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class OrderService :IOrderService
    {
        private readonly InventoryContext context;
        private readonly IMapper mapper;

        public OrderService(InventoryContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Create(OrdersDTO ordersDTO)
        {
            Orders newOrder = mapper.Map<Orders>(ordersDTO);

            context.Orders.Add(newOrder);
            context.SaveChanges();
        }

        public void Update(OrdersDTO ordersDTO)
        {
            Orders newOrder = mapper.Map<Orders>(ordersDTO);

            context.Orders.Attach(newOrder);
            context.Entry(newOrder).State = EntityState.Modified;
            context.SaveChanges();
        }

        public PaginatedList<OrdersDTO> GetAll(int CurrentPage)
        {
            List<Orders> allOrders = context.Orders.Include(o => o.Product).Include(o => o.Customer).ToList();
            List<OrdersDTO> Orders = mapper.Map<List<OrdersDTO>>(allOrders);

            PaginatedList<OrdersDTO> PaginatPatients = PaginatedList<OrdersDTO>.CreateAsync(Orders, CurrentPage);

            return PaginatPatients;
        }

        public List<OrdersDTO> GetAll()
        {
            List<Orders> allOrders = context.Orders.Include(o => o.Product).Include(o => o.Customer).ToList();
            List<OrdersDTO> Orders = mapper.Map<List<OrdersDTO>>(allOrders);
            return Orders;
        }

        public OrdersDTO Get(int Id)
        {
            Orders Order = context.Orders.Find(Id);
            OrdersDTO ordersDTO = mapper.Map<OrdersDTO>(Order);

            return ordersDTO;
        }

        public void Delete(int Id)
        {
            Orders order = context.Orders.Find(Id);

            context.Orders.Remove(order);
            context.SaveChanges();
        }
        public List<Orders> GetOrder()
        {
            List<Orders> li = context.Orders.Include(o => o.Product).Include(o => o.Customer).ToList();

            return li;
        }
        public PaginatedList<OrdersDTO> loadbyname(string name, int CurrentPage)
        {
            List<Orders> li = context.Orders.Include(o => o.Product).Include(o => o.Customer).Where(p => p.Product.Name == name).ToList();

            List<OrdersDTO> orders = mapper.Map<List<OrdersDTO>>(li);

            PaginatedList<OrdersDTO> Paginatorders = PaginatedList<OrdersDTO>.CreateAsync(orders, CurrentPage);

            return Paginatorders;
        }
    }
}

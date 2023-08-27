using ClosedXML.Excel;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using InventorySystem.services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventorySystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly IProductService productService;

        public OrdersController(IOrderService _orderService, ICustomerService _customerService, IProductService _productService)
        {
            orderService = _orderService;
            customerService = _customerService;
            productService = _productService;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<OrdersDTO> orders = orderService.GetAll(CurrentPage);

            return View("OrdersList", orders);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {
            PaginatedList<OrdersDTO> orders = orderService.GetAll(CurrentPage);

            return View("OrdersList", orders);
        }
        public IActionResult NewOrder()
        {
            vmOrders vm = new vmOrders();
            vm.LiProduct = productService.GetProduct();
            vm.LiCustomer = customerService.GetCustomer();

            ViewData["IsEdit"] = false;
            return View("OrdersAdd", vm);
        }

        public IActionResult GetProductInfo(int ProductId)
        {            
            return Json(productService.Get(ProductId));
        }

        public IActionResult Save(vmOrders vm)
        {
            try { 
            orderService.Create(vm.Order);
            vm.LiProduct = productService.GetProduct();
            vm.LiCustomer = customerService.GetCustomer();
            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Order Created Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Adding Order";
                ViewData["Status"] = "Error";
            }
            return View("OrdersAdd", vm);
        }

        public IActionResult Update(vmOrders vm)
        {
            try
            {
                orderService.Update(vm.Order);
                vm.LiProduct = productService.GetProduct();
                vm.LiCustomer = customerService.GetCustomer();
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Order Updated Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Order";
                ViewData["Status"] = "Error";
            }
            return View("OrdersAdd", vm);
        }

        public IActionResult Edit(int Id)
        {
            vmOrders vm = new vmOrders();
            OrdersDTO Order = orderService.Get(Id);
            vm.Order = Order;
            vm.LiProduct = productService.GetProduct();
            vm.LiCustomer = customerService.GetCustomer();

            ViewData["IsEdit"] = true;
            return View("OrdersAdd", vm);
        }

        public IActionResult Delete(int Id)
        {
            orderService.Delete(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Orders");
            dt.Columns.AddRange(new DataColumn[8]
            {
              new DataColumn("Id"),
              new DataColumn("Customer"),
              new DataColumn("Product"),
              new DataColumn("Qty"),
              new DataColumn("OrderDate"),
              new DataColumn("Price"),
              new DataColumn("Discount"),
              new DataColumn("PaymentMethod")
              
            });

            var orders = orderService.GetOrder();

            foreach (var order in orders)
            {
                dt.Rows.Add(order.Id, order.Customer.Name, order.Product.Name, order.Qty, order.OrderDate, order.Price, order.Discount,order.PaymentMethod);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
                }
            }

        }

        public IActionResult search(int CurrentPage = 1)
        {
            string name = Request.Form["name"];
            PaginatedList<OrdersDTO> orders = orderService.loadbyname(name, CurrentPage);
            return View("OrdersList",orders);
          
        }
    }
}

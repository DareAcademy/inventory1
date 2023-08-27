using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventorySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly IStoresService storesService;
        private readonly IAccountService accountService;
        private readonly ICustomerService customerService;
        private readonly ISupplierService supplierService;
        private readonly IOrderService orderService;

        public HomeController(ILogger<HomeController> logger,
                              IProductService _ProductService,
                              IStoresService _StoresService,
                              IAccountService _AccountService,
                              ICustomerService _CustomerService,
                              ISupplierService _SupplierService,
                              IOrderService _OrderService
                              )
        {
            _logger = logger;
            productService = _ProductService;
            storesService = _StoresService;
            accountService = _AccountService;
            customerService = _CustomerService;
            supplierService = _SupplierService;
            orderService = _OrderService;
        }

        public async Task<IActionResult> Index()
        {
            Dashboard dashboard = new Dashboard();
            dashboard.TotalCustomers = customerService.GetAll().Count();
            dashboard.TotalSupplier = supplierService.GetAll().Count();
            List<UserDTO> users = await accountService.GetUsers();
            dashboard.TotalUsers = users.Count();
            dashboard.TotalStores = storesService.GetAll().Count();
            dashboard.TotalProducts = productService.GetAll().Count();
            dashboard.TotalSales = orderService.GetAll().Sum(o => o.Price);
            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
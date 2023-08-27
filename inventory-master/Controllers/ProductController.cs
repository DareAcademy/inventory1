using ClosedXML.Excel;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using InventorySystem.services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;
        private readonly ICategoryService categoryService;
        private readonly IConfiguration config;
        public ProductController(IProductService _productService , ISupplierService _supplierService, ICategoryService _categoryService, IConfiguration _config)
        {
            productService = _productService;
            supplierService = _supplierService;
            categoryService = _categoryService;
            config = _config;


        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<ProductDTO> products = productService.GetAll(CurrentPage);

            return View("ProductList", products);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {
            PaginatedList<ProductDTO> products = productService.GetAll(CurrentPage);

            return View("ProductList", products);
        }
        public IActionResult NewProduct()
        {
            vmProduct vm = new vmProduct();
            vm.LiSupplier = supplierService.GetSupplier();
            vm.LiCategory = categoryService.GetCategory();
            ViewData["IsEdit"] = false;
            return View("ProductAdd", vm);
        }

        public IActionResult Save(vmProduct vm)
        {
            try { 
            if (vm.product.image != null)
            {
                string fileName = Guid.NewGuid().ToString() + vm.product.image.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                vm.product.image.CopyTo(new FileStream(path, FileMode.Create));
                vm.product.path = fileName;
            }
            productService.Create(vm.product);
            vm.LiSupplier = supplierService.GetSupplier();
            vm.LiCategory = categoryService.GetCategory();
            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Product Created Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Adding Product";
                ViewData["Status"] = "Error";
            }
            return View("ProductAdd",vm);
        }

        public IActionResult Update(vmProduct vm)
        {
            try { 
            if (vm.product.image != null)
            {
                string fileName = Guid.NewGuid().ToString() + vm.product.image.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                vm.product.image.CopyTo(new FileStream(path, FileMode.Create));
                vm.product.path = fileName;
            }
            productService.Update(vm.product);
            vm.LiSupplier = supplierService.GetSupplier();
            vm.LiCategory = categoryService.GetCategory();
         

            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Product Updated Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Product";
                ViewData["Status"] = "Error";
            }
            return View("ProductAdd", vm);
        }

        public IActionResult Edit(int Id)
        {
            vmProduct vm = new vmProduct();
            ProductDTO product = productService.Get(Id);
            vm.product = product;
            vm.LiSupplier = supplierService.GetSupplier();
            vm.LiCategory = categoryService.GetCategory();
            ViewData["IsEdit"] = true;
            return View("ProductAdd", vm);

        }

        public IActionResult Delete(int Id)
        {
            productService.Delete(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Products");
            dt.Columns.AddRange(new DataColumn[8]

            {
              new DataColumn("Id"),
              new DataColumn("Name"),
              new DataColumn("SellPrice"),
              new DataColumn("BuyingPrice"),
              new DataColumn("Qty"),
              new DataColumn("Category"),
              new DataColumn("Supplier"),
              new DataColumn("IsAvailable"),
             });

            var products = productService.GetProduct();

            foreach (var product in products)
            {
                dt.Rows.Add(product.Id, product.Name, product.SellPrice, product.BuyingPrice,product.Qty, product.Category.Name, product.Supplier.Name, product.IsAvailable);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Product.xlsx");
                }
            }

        }
        public IActionResult search(int CurrentPage = 1)
        {
            string name = Request.Form["Name"];
            PaginatedList<ProductDTO> products = productService.loadbyname(name, CurrentPage);
            return View("ProductList", products);
        }
    }
}

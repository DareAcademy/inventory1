using ClosedXML.Excel;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using InventorySystem.services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventorySystem.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService supplierService;
        public SupplierController(ISupplierService _supplierService)
        {
            supplierService = _supplierService;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<SupplierDTO> suppliers = supplierService.GetAll(CurrentPage);
            return View("SupplierList", suppliers);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {
            PaginatedList<SupplierDTO> suppliers = supplierService.GetAll(CurrentPage);

            return View("SupplierList", suppliers);
        }
        public IActionResult NewSupplier()
        {
            ViewData["IsEdit"] = false;
            return View("SupplierAdd");
        }

        public IActionResult Save(SupplierDTO supplier)
        {
            try { 
            supplierService.Create(supplier);
            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Supplier Created Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Adding Supplier";
                ViewData["Status"] = "Error";
            }
            return View("SupplierAdd");
        }

        public IActionResult Update(SupplierDTO supplier)
        {
            try
            {
                supplierService.Update(supplier);
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Supplier Updated Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Supplier";
                ViewData["Status"] = "Error";
            }
            return View("SupplierAdd");
        }

        public IActionResult Edit(int Id)
        {
            SupplierDTO supplier = supplierService.Get(Id);
            ViewData["IsEdit"] = true;
            return View("SupplierAdd", supplier);
        }

        public IActionResult Delete(int Id)
        {
            supplierService.Delete(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Suppliers");
            dt.Columns.AddRange(new DataColumn[4] { 
              new DataColumn("Id"),
             new DataColumn("Name"),
             new DataColumn("Email"),
             new DataColumn("Address"),
            });



            var suppliers = supplierService.GetSupplier();

            foreach (var supplier in suppliers)
            {
                dt.Rows.Add(supplier.Id, supplier.Name, supplier.Email, supplier.Address);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Suppliers.xlsx");
                }
            }

        }
        public IActionResult search(int CurrentPage = 1)
        {
            string name = Request.Form["Name"];
            PaginatedList<SupplierDTO> suppliers = supplierService.loadbyname(name, CurrentPage);
            return View("SupplierList", suppliers);
        }
    }
}

    


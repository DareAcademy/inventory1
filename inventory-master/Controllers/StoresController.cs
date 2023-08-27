using ClosedXML.Excel;
using InventorySystem.Data.Enum;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using InventorySystem.services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventorySystem.Controllers
{
    public class StoresController : Controller
    {
        private readonly IStoresService storesService;
        private readonly ICompanyService companyService;

        public StoresController(IStoresService _storesService, ICompanyService _companyService)
        {
            storesService = _storesService;
            companyService = _companyService;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<StoresDTO> companies = storesService.GetAll(CurrentPage);

            return View("StoresList", companies);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {
            PaginatedList<StoresDTO> Stores = storesService.GetAll(CurrentPage);

            return View("StoresList", Stores);
        }
        public IActionResult NewStore()
        {
            vmStores vm = new vmStores();
            vm.LiCompany = companyService.GetCompany();
           
            ViewData["IsEdit"] = false;
            return View("StoresAdd",vm);
        }

        public IActionResult Save(vmStores vm)
        {
            try { 
            storesService.Create(vm.Store);

            vm.LiCompany = companyService.GetCompany();
            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Company Created Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Adding Company";
                ViewData["Status"] = "Error";
            }
            return View("StoresAdd",vm);
        }

        public IActionResult Update(vmStores vm)
        {
            try { 
            storesService.Update(vm.Store);
            vm.LiCompany = companyService.GetCompany();
            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Store Updated Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Store";
                ViewData["Status"] = "Error";
            }
            return View("StoresAdd",vm);
        }

        public IActionResult Edit(int Id)
        {
            vmStores vm =new vmStores();
            StoresDTO Store = storesService.Get(Id);
            vm.Store = Store;
            vm.LiCompany = companyService.GetCompany();
            ViewData["IsEdit"] = true;
            return View("StoresAdd", vm);
        }

        public IActionResult Delete(int Id)
        {
            storesService.Delete(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Stores");
            dt.Columns.AddRange(new DataColumn[6]

            {
              new DataColumn("Id"),
              new DataColumn("Name"),
              new DataColumn("phone"),
              new DataColumn("Address"),
              new DataColumn("Status"),
              new DataColumn("Company"),
 });

            var stores = storesService.GetStore();

            foreach (var store in stores)
            {
                dt.Rows.Add(store.Id, store.Name, store.phone, store.Address, store.Status, store.Company.Name);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Stores.xlsx");
                }
            }

        }
        public IActionResult search(int CurrentPage = 1)
        {
            string name = Request.Form["Name"];
            PaginatedList<StoresDTO> stores = storesService.loadbyname(name, CurrentPage);
            return View("StoresList", stores);
        }
    }
}

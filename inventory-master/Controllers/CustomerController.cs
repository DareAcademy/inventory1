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
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService _customerService)
        {
            customerService = _customerService;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<CustomerDTO> customers = customerService.GetAll(CurrentPage);

            return View("CustomerList", customers);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {
            PaginatedList<CustomerDTO> customers = customerService.GetAll(CurrentPage);

            return View("CustomerList", customers);
        }
        public IActionResult NewCustomer()
        {
            ViewData["IsEdit"] = false;
            return View("CustomerAdd");
        }

        public IActionResult Save(CustomerDTO company)
        {
            try { 
            customerService.Create(company);
            ViewData["IsEdit"] = true;
            ViewData["AlertMessage"] = "Customer Created Successfully";
            ViewData["Status"] = "Success";
        }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Adding Customer";
                ViewData["Status"] = "Error";
            }
            return View("CustomerAdd");
        }

        public IActionResult Update(CustomerDTO company)
        {
            try
            {
                customerService.Update(company);
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Customer Updated Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Customer";
                ViewData["Status"] = "Error";
            }
            return View("CustomerAdd");
        }

        public IActionResult Edit(int Id)
        {
            CustomerDTO Company = customerService.Get(Id);
            ViewData["IsEdit"] = true;
            return View("CustomerAdd", Company);
        }

        public IActionResult Delete(int Id)
        {
            customerService.Delete(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Customer");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                          
                                             new DataColumn("Email"),
                                             new DataColumn("Type"),
                                             new DataColumn("Address") });

            var customers = customerService.GetCustomer();

            foreach (var customer in customers)
            {
                dt.Rows.Add(customer.Id, customer.Name, customer.Address, customer.Email,customer.Address);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customer.xlsx");
                }
            }

        }
        public IActionResult search(int CurrentPage = 1)
        {
            string name = Request.Form["Name"];
            PaginatedList<CustomerDTO> customers = customerService.loadbyname(name, CurrentPage);
            return View("CustomerList", customers);
        }
    }
}

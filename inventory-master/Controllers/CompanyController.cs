using ClosedXML.Excel;
using InventorySystem.Data;
using InventorySystem.helpers;
using InventorySystem.IServices;
using InventorySystem.Models;
using InventorySystem.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Globalization;

namespace InventorySystem.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService _companyService)
        {
            companyService = _companyService;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<CompanyDTO> companies = companyService.GetAll(CurrentPage);

            return View("CompanyList", companies);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {
            PaginatedList<CompanyDTO> companies = companyService.GetAll(CurrentPage);

            return View("CompanyList", companies);
        }
        public IActionResult NewCompany()
        {
            ViewData["IsEdit"] = false;
            return View("CompanyAdd");
        }

        public IActionResult Save(CompanyDTO company)
        {
            try
            {
                companyService.Create(company);
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Company Created Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Adding Company";
                ViewData["Status"] = "Error";
            }
            return View("CompanyAdd");
        }

        public IActionResult Update(CompanyDTO company)
        {
            try
            {
                companyService.Update(company);
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Company Updated Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Company";
                ViewData["Status"] = "Error";
            }

            return View("CompanyAdd");
        }

        public IActionResult Edit(int Id)
        {
            CompanyDTO Company = companyService.Get(Id);
            ViewData["IsEdit"] = true;
            return View("CompanyAdd", Company);
        }

        public IActionResult Delete(int Id)
        {
            try
            {
                companyService.Delete(Id);
                //ViewData["AlertMessage"] = "Company Deleted Successfully";
                //ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                //ViewData["AlertMessage"] = "Error While deleting Company";
                //ViewData["Status"] = "Error";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Export()
        {
            DataTable dt = new DataTable("Company");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("phone"),
                                             new DataColumn("TaxPersent"),
                                            new DataColumn("Address") });

            var copmanies = companyService.GetCompany();

            foreach (var company in copmanies)
            {
                dt.Rows.Add(company.Id, company.Name, company.phone, company.Address, company.TaxPersent);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Company.xlsx");
                }
            }

        }


    }
}

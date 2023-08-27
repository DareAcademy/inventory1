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
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult Index(int CurrentPage = 1)
        {
            PaginatedList<CategoryDTO> categorys = categoryService.GetAll(CurrentPage);

            return View("CategoryList", categorys);
        }

        public IActionResult PagginatedList(int CurrentPage = 1)
        {

            PaginatedList<CategoryDTO> categorys = categoryService.GetAll(CurrentPage);

            return View("CategoryList", categorys);
        }
        public IActionResult NewCategory()
        {
            ViewData["IsEdit"] = false;
            return View("CategoryAdd");
        }

        public IActionResult Save(CategoryDTO category)
        {
            try
            {
                categoryService.Create(category);
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Category Added Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While adding Category";
                ViewData["Status"] = "Error";
            }
            return View("CategoryAdd");
        }

        public IActionResult Update(CategoryDTO category)
        {
            try
            {
                categoryService.Update(category);
                ViewData["IsEdit"] = true;
                ViewData["AlertMessage"] = "Category Updated Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating Category";
                ViewData["Status"] = "Error";
            }
            return View("CategoryAdd");
        }

        public IActionResult Edit(int Id)
        {
            CategoryDTO category = categoryService.Get(Id);
            ViewData["IsEdit"] = true;
            return View("CategoryAdd", category);
        }

        public IActionResult Delete(int Id)
        {
            categoryService.Delete(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Categorys");
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"),
                                            new DataColumn("Name"),
                                            new DataColumn("phone") });
                                            
              

            var categories = categoryService.GetCategory();

            foreach (var category in categories)
            {
                dt.Rows.Add(category.Id, category.Name, category.Status);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Category.xlsx");
                }
            }
        }

        public IActionResult  search ( int CurrentPage=1)
        {
            string name = Request.Form["Name"];
            PaginatedList<CategoryDTO> categorys = categoryService.loadbyname(name, CurrentPage);
            return View("CategoryList", categorys);
        }

    }
}

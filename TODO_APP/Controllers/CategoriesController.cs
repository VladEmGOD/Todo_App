using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TODO_APP.Models;
using TODO_APP.Repositories;
using TODO_APP.Repositories.Infrastructure;
using System.IO;
using Buisness.Repositories.Interfaces;
using Buisness.Models;

namespace TODO_APP.Controllers
{
    public class CategoriesController : Controller
    {
        CategoryReslover categoriesResolver;
        ICategoriesPerository categoriesRepository;
        public CategoriesController(CategoryReslover c)
        {
            categoriesResolver = c;
        }

        public async Task<IActionResult> Index()
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            categoriesRepository = categoriesResolver(dataSource);

            var categories = await categoriesRepository.GetCategoriesAsync();
            
            HttpContext.Session.SetJson("DataSource", dataSource);
            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            categoriesRepository = categoriesResolver(dataSource);

            if (ModelState.IsValid)
            {
                var categories = await categoriesRepository.GetCategoriesAsync();
                bool isExist = false;

                foreach (CategoryModel c in categories)
                {
                    if (c.Name.ToLower() == categoryModel.Name.ToLower())
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    ModelState.AddModelError("", "That category already exist!");
                    return View();
                }

                await categoriesRepository.CreateAsync(categoryModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            categoriesRepository = categoriesResolver(dataSource);

            var todo = await categoriesRepository.GetCategoryByIdAsync(id);
            if (todo != null)
            {
                await categoriesRepository.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            categoriesRepository = categoriesResolver(dataSource);

            var category = await categoriesRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel categoryModel)
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            categoriesRepository = categoriesResolver(dataSource);

            if (ModelState.IsValid)
            {
                await categoriesRepository.EditAsync(categoryModel);
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

        public IActionResult ChangeDataSource(string source) 
        {
            if (source == "XML") 
            {
                HttpContext.Session.SetJson("DataSource", "XML");
                categoriesRepository = categoriesResolver("XML");
                return RedirectToAction("Index");
            }

            if (source == "MsSql")
            {
                HttpContext.Session.SetJson("DataSource", "MsSql");
                categoriesRepository = categoriesResolver("MsSql");
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}

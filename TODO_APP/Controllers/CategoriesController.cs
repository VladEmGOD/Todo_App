using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;
using TODO_APP.Repositories;

namespace TODO_APP.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoriesPerository repo;
        public CategoriesController(ICategoriesPerository c)
        {
            repo = c;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await repo.GetCategoriesAsync();
            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel cmodel)
        {
            if (ModelState.IsValid)
            {
                var category = await repo.GetCategoriesAsync();
                bool isExist = false;

                foreach (CategoryModel c in category)
                {
                    if (c.NameC.ToLower() == cmodel.NameC.ToLower())
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

                await repo.CreateAsync(cmodel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await repo.GetAsync(id);
            if (todo != null)
            {
                await repo.DeleteAsync(id);
                TempData["Success"] = "Todo was deleted";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "That todo not exist";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await repo.GetAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel cmodel) 
        {
            if (ModelState.IsValid) 
            {
                await repo.UpdateAsync(cmodel);
                return RedirectToAction("Index");
            }

            return View(cmodel);
        }
    }
}

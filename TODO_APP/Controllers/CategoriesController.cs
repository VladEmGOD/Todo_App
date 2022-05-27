using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Buisness.Repositories.Interfaces;
using Buisness.Models;
using Microsoft.AspNetCore.Http;
using Buisness;
using TODO_APP.Infrastructure;

namespace TODO_APP.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoriesRerository categoriesRepository;
        DataSource dataSource;

        public CategoriesController(RepositoryResolver repositoryReslover, IHttpContextAccessor httpContextAccesor)
        {
            string? dataSourceStr = httpContextAccesor.HttpContext.Request.Cookies["DataSource"];
            bool isSourceValid = Enum.TryParse(dataSourceStr, out dataSource);
            if (isSourceValid)
            {
                categoriesRepository = repositoryReslover.ResolveCategoryRepository(dataSource);
            }
            else
            {
                categoriesRepository = repositoryReslover.ResolveCategoryRepository(DataSource.MsSql);
            }
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoriesRepository.GetCategoriesAsync();
            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
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
            var category = await categoriesRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                await categoriesRepository.EditAsync(categoryModel);
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

    }
}

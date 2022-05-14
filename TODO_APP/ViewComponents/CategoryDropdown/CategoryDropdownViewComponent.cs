using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Repositories;
using TODO_APP.Repositories.Infrastructure;

namespace TODO_APP.ViewComponents.CategoryDropdown
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        CategoryReslover categoriesResolver;
        ICategoriesPerository categoriesRepository;
        public CategoryDropdownViewComponent(CategoryReslover c) 
        {
            string dataSource = HttpContext.Session.GetJson<string>("DataSource") ?? "MsSql";
            categoriesResolver = c;
            categoriesRepository = categoriesResolver(dataSource);
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoriesRepository.GetCategoriesAsync();
            return View(categories);
        }
    }
}

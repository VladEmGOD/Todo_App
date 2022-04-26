using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Repositories;

namespace TODO_APP.ViewComponents.CategoryDropdown
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        ICategoriesPerository repo;
        public CategoryDropdownViewComponent(ICategoriesPerository c) 
        {
            repo = c;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await repo.GetCategoriesAsync();
            return View(categories);
        }
    }
}

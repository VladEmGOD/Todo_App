using Buisness.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TODO_APP.Infrastructure;

namespace TODO_APP.ViewComponents.CategoryDropdown
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        ICategoriesRerository categoriesRepository;
        DataSource dataSource;
        public CategoryDropdownViewComponent(RepositoryResolver repositoryReslover, IHttpContextAccessor httpContextAccesor) 
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoriesRepository.GetCategoriesAsync();
            return View(categories);
        }
    }
}

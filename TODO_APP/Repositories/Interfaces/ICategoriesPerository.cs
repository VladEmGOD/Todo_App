using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;

namespace TODO_APP.Repositories
{
    public interface ICategoriesPerository
    {
        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();
        Task CreateAsync(CategoryModel category);
        Task<CategoryModel> GetCategoryByIdAsync(int id);
        Task EditAsync(CategoryModel category);
        Task DeleteAsync(int id);

    }
}

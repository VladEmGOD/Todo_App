using Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buisness.Repositories.Interfaces
{
    public interface ICategoriesRerository
    {
        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();
        Task<CategoryModel> CreateAsync(CategoryModel category);
        Task<CategoryModel> GetCategoryByIdAsync(int id);
        Task EditAsync(CategoryModel category);
        Task DeleteAsync(int id);

    }
}

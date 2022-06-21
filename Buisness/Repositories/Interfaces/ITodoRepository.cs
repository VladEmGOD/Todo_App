using Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buisness.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoModel> CreateAsync(TodoModel todo);
        Task DeleteAsync(int id);
        Task<TodoModel> GetTodoByIdAsync(int id);
        Task<IEnumerable<TodoModel>> GetTodosAsync();
        Task<IEnumerable<TodoModel>> GetTodosByPaginationAsync(int page, int pageSize);
        Task UpdateAsync(TodoModel todo);
        Task<IEnumerable<TodoModel>> GetTodoByCategoryAsync(int id);
    }
}

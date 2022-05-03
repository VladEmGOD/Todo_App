using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;

namespace TODO_APP.Repositories
{
    public interface ITodoRepository
    {
        Task CreateAsync(TodoModel todo);
        Task DeleteAsync(int id);
        Task<TodoModel> GetAsync(int id);
        Task<List<TodoModel>> GetTodosAsync();
        Task<List<TodoModel>> GetTodosByPaginationAsync(int page, int pageSize);
        Task UpdateAsync(TodoModel todo);
        Task<List<TodoModel>> GetTodoByCategoryAsync(int id);
    }
}

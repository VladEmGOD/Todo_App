using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Buisness.Repositories.Interfaces;
using Buisness.Models;

namespace MsSQL.Repositories
{
    public class MsSqlTodoRepository : ITodoRepository
    {
        private IDbConnection DbConection = null;
        public MsSqlTodoRepository(IDbConnection conection)
        {
            DbConection = conection;
        }

        public Task CreateAsync(TodoModel todo)
        {
            if (todo.CategoryId == 0) todo.CategoryId = null;
            string sqlquery = @"insert into todos(CategoryId, Tittle, isDone, Deadline)
                                values (@CategoryId, @Tittle, @isDone, @Deadline)";
            return DbConection.ExecuteAsync(sqlquery, todo);
        }

        public Task DeleteAsync(int id)
        {
            return DbConection.QueryAsync<TodoModel>($"delete from Todos where id = @id", new { id });
        }

        public Task<TodoModel> GetTodoByIdAsync(int id)
        {
            return DbConection.QuerySingleAsync<TodoModel>($"select * from Todos where Id = @id", new { id });
        }

        public Task<IEnumerable<TodoModel>> GetTodosAsync()
        {
            string query = $"select * from Todos";
            return DbConection.QueryAsync<TodoModel>(query);
        }

        public Task UpdateAsync(TodoModel todo)
        {
            int isDone = todo.IsDone ? 1 : 0;

            if (todo.CategoryId == 0) todo.CategoryId = null;
            var query = @"update Todos set CategoryId = @CategoryId, Tittle = @Tittle,
                              Deadline = @Deadline, isDone = @isDone
                              where id = @Id";
            return DbConection.QueryAsync<TodoModel>(query, todo);
        }

        public Task<IEnumerable<TodoModel>> GetTodoByCategoryAsync(int id)
        {
            string query = "select * from Todos where CategoryId = @id";
            return DbConection.QueryAsync<TodoModel>(query, new { id });
        }

        public Task<IEnumerable<TodoModel>> GetTodosByPaginationAsync(int page, int pageSize)
        {
            string query = @"select * from Todos
                                order by id
                                OFFSET @page*@pageSize ROWS
                                FETCH NEXT @pageSize ROWS ONLY";
            return DbConection.QueryAsync<TodoModel>(query, new { page, pageSize });
        }
    }
}

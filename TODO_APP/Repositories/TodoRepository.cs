using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using TODO_APP.Models;
using Microsoft.Data.SqlClient;

namespace TODO_APP.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private string _connectionString = null;
        public TodoRepository(string conn)
        {
            _connectionString = conn;
        }

        public async Task CreateAsync(TodoModel todo)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlquery;
                if (todo.CategoryId != 0)
                {
                    sqlquery = @"insert into todos(CategoryId, Tittle, DescriptionT, isDone, Deadline)
                                 values (@CategoryId, @Tittle, @DescriptionT, @isDone, @Deadline)";
                }
                else 
                {
                    sqlquery = @"insert into todos(Tittle, DescriptionT, isDone, Deadline)
                                 values (@Tittle, @DescriptionT, @isDone, @Deadline)";
                }

                await db.ExecuteAsync(sqlquery, todo);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync<TodoModel>($"delete from Todos where id = @id", new { id });
            }
        }

        public async Task<TodoModel> GetAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<TodoModel>($"select * from Todos WHERE Id = @id", new { id });
            }
        }

        public async Task<List<TodoModel>> GetTodosAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"select * from Todos";
                return (await db.QueryAsync<TodoModel>(query)).ToList();
            }
        }

        public async Task UpdateAsync(TodoModel t)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int isDone = t.isDone ? 1 : 0;
                var query = @"update Todos set CategoryId = @CategoryId, Tittle = @Tittle,
                              DescriptionT = @DescriptionT, Deadline = @Deadline, isDone = @isDone
                              where id = @id";

                await db.QueryAsync<TodoModel>(query, t);
            }
        }

        public async Task<List<TodoModel>> GetTodoByCategoryAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Todos where CategoryId = @id";
                return (await db.QueryAsync<TodoModel>(query, new { id })).ToList();
            }
        }

        public async Task<List<TodoModel>> GetTodosByPaginationAsync(int page, int pageSize)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"select * from Todos
                                order by id
                                OFFSET @page*@pageSize ROWS
                                FETCH NEXT @pageSize ROWS ONLY";
                return (await db.QueryAsync<TodoModel>(query, new { page, pageSize })).ToList();
            }
        }
    }
}

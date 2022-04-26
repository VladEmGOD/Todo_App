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
                int isDone = todo.isDone ? 1 : 0;
                var sqlQuery = $"insert into Todos(CategoryId, Tittle, DescriptionT, isDone, Deadline)" +
                    $"values ({todo.CategoryId}, '{todo.Tittle}', '{todo.DescriptionT}', {isDone}, '{todo.Deadline}')";
                await db.QueryAsync(sqlQuery);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync<TodoModel>($"delete from Todos where id = {id}");
            }
        }

        public async Task<TodoModel> GetAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<TodoModel>($"select * from Todos WHERE Id = {id}");
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
                await db.QueryAsync<TodoModel>($"update Todos set CategoryId = '{t.CategoryId}', Tittle = '{t.Tittle}'," +
                                               $"DescriptionT = '{t.DescriptionT}', Deadline = '{t.Deadline}', isDone = {isDone}" +
                                               $"where id = {t.id}");
            }
        }

        public async Task<List<TodoModel>> GetTodoByCategoryAsync(int id) 
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"select * from Todos where CategoryId = {id}";
                return (await db.QueryAsync<TodoModel>(query)).ToList();
            }
        }

    }
}

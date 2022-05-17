using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Buisness.Models;
using Buisness.Repositories.Interfaces;

namespace MsSQL.Repositories
{
    public class MsSqlCategoriesRepository : ICategoriesRerository
    {
        private IDbConnection DbConnection = null;
        public MsSqlCategoriesRepository(IDbConnection db)
        {
            DbConnection = db;
        }

        public Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            string query = @"select * from Categories";
            return DbConnection.QueryAsync<CategoryModel>(query);
        }

        public Task CreateAsync(CategoryModel category)
        {
            string query = @"insert into Categories(name) values (@Name)";
            return DbConnection.QueryAsync(query, category);
        }

        public Task EditAsync(CategoryModel category)
        {
            string query = @"update Categories set Name = @Name where Id = @id";
            return DbConnection.QueryAsync(query, category);
        }

        public Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            string query = @"select * from Categories where Id = @id";
            return DbConnection.QueryFirstOrDefaultAsync<CategoryModel>(query, new { id });
        }

        public Task DeleteAsync(int id)
        {
            return DbConnection.QueryAsync<CategoryModel>(@"delete from Categories where Id = @id", new { id });
        }

    }
}

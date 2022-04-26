﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TODO_APP.Models;
using Dapper;

namespace TODO_APP.Repositories
{
    public class ICategoriesRepository : ICategoriesPerository
    {
        private string _connectionString = null;
        public ICategoriesRepository(string conn)
        {
            _connectionString = conn;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"select * from Categories";
                return (await db.QueryAsync<CategoryModel>(query)).ToList();
            }
        }

        public async Task CreateAsync(CategoryModel category)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"insert into Categories(NameC) values ('{category.NameC}')";
                await db.QueryAsync(query);
            }
        }

        public async Task EditAsync(CategoryModel category)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"update Categories set NameC = '{category.NameC}' where id = {category.id}";
                await db.QueryAsync(query);
            }
        }

        public async Task<CategoryModel> GetAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"select * from Categories where id = {id}";
                return await db.QueryFirstOrDefaultAsync<CategoryModel>(query);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync<CategoryModel>($"delete from Categories where id = {id}");
            }
        }

        public async Task UpdateAsync(CategoryModel cmodel)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync<CategoryModel>($"update Categories set NameC = '{cmodel.NameC}' " +
                                                   $"where id = {cmodel.id}");
            }
        }
    }
}
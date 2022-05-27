using Buisness.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MsSQL.Repositories;
using System;
using TODO_APP.Repositories.XML;

namespace GraphQL_API.Infrastructure
{
    public class RepositoryResolver
    {
        IServiceProvider serviceProvider;
        public RepositoryResolver(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        public ICategoriesRerository ResolveCategoryRepository(DataSource dataSource) 
        {
            switch (dataSource)
            {
                case DataSource.MsSql:
                    return serviceProvider.GetService<MsSqlCategoriesRepository>();
                case DataSource.XML:
                    return serviceProvider.GetService<XMLCategoriesRepository>();
                default:
                    return null;
            }
        }

        public ITodoRepository ResolveTodoRepository(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.MsSql:
                    return serviceProvider.GetService<MsSqlTodoRepository>();
                case DataSource.XML:
                    return serviceProvider.GetService<XMLTodoReository>();
                default:
                    return null;
            }
        }
    }
}

using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MsSQL.Repositories;
using System;
using System.Data;
using TODO_APP.GraphQL.Mutations;
using TODO_APP.GraphQL.Queries;
using TODO_APP.GraphQL.Schemas;
using TODO_APP.Infrastructure;
using TODO_APP.Repositories.XML;

namespace TODO_APP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            string connectionString = Configuration.GetConnectionString("TODO_DB");
            services.AddTransient<IDbConnection>(provider => new SqlConnection(connectionString));
            services.AddTransient<RepositoryResolver>();
            //MSSQL dependencies 
            services.AddScoped<MsSqlTodoRepository>();
            services.AddScoped<MsSqlCategoriesRepository>();
            //XML dependencies 
            services.AddScoped<XMLTodoReository>();
            services.AddScoped<XMLCategoriesRepository>();

            //GraphQl
            services.AddScoped<AppSchema>();
            services.AddScoped<TodoAppQuery>();
            services.AddScoped<TodoAppMutation>();

            services.AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Transient);

            services.AddControllersWithViews();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Todo/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseGraphQL<AppSchema, GraphQLHttpMiddleware<AppSchema>>();
            
            app.UseGraphQLAltair();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/{controller=Todo}/{action=Index}/{id?}");
            });
        }
    }
}

using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL_API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MsSQL.Repositories;
using System.Data;
using TODO_APP.Repositories.XML;

namespace GraphQL_API
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
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();

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

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                    .WithMethods("POST")
                    .AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseGraphQL<AppSchema, GraphQLHttpMiddleware<AppSchema>>();
            app.UseGraphQLAltair();
            app.UseCors();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}

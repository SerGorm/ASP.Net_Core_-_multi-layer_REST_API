using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using WebTestApplication5.DAL.Interfaces;
using WebTestApplication5.DAL.Repositories;
using WebTestApplication5.BLL.Interfaces;
using WebTestApplication5.BLL.Services;

namespace WebTestApplication5.WEB
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=REST_API_TEST_DB_5;Trusted_Connection=True;";
            //string connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=userstore;Integrated Security=True";
            var uow = new EFUnitOfWork(connectionString);
            //services.AddTransient<IUnitOfWork, EFUnitOfWork>(provider);
            services.AddTransient<IActionService, ActionService>(provider => new ActionService(uow));
            services.AddTransient<IRoleService, RoleService>(provider => new RoleService(uow));
            services.AddTransient<ICompanyUserRolesService, CompanyUserRolesService>(provider => new CompanyUserRolesService(uow));

            services.AddControllers();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app/*, IWebHostEnvironment env*/)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My REST API V1");
            });

            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                /*endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });*/
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace EmployeeManagement
{
    public class Startup
    {
        #region Reading Configuration Settings
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        #endregion Reading Configuration Settings


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDbContext>();

            //services.AddMvc();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            
            services.AddScoped<IEmpolyeeRepository, SQLEmployeeRepository>();
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
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });




            #region Original Code
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("hi there..");
            //    });
            //});
            #endregion Original Code
        }






        #region test
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEmpolyeeRepository employeeRepository)
        //{
        //    System.Diagnostics.Debug.WriteLine("@**"+employeeRepository.GetEmployee(2).Email+"**");
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Error");
        //        app.UseStatusCodePagesWithRedirects("/Error/{0}");
        //    }

        //    app.UseStaticFiles();

        //    //app.UseMvcWithDefaultRoute();
        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        //    });




        //    #region Original Code
        //    //app.UseRouting();

        //    //app.UseEndpoints(endpoints =>
        //    //{
        //    //    endpoints.MapGet("/", async context =>
        //    //    {
        //    //        await context.Response.WriteAsync("hi there..");
        //    //    });
        //    //});
        //    #endregion Original Code
        //}
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
    public class Startup
    
       {
           public Startup(IConfiguration configuration) =>
               Configuration = configuration;
           public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
           {
               services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
                       Configuration["Data:SportStoreProducts:ConnectionString"]));
               services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();
        }

           public void Configure(IApplicationBuilder app, IHostingEnvironment env)
           {
               app.UseDeveloperExceptionPage();
               app.UseStatusCodePages();
               app.UseStaticFiles();
               app.UseMvc(routes => {
                   routes.MapRoute(
                       name: "pagination",
                       template: "Produkty/Strona{productPage}",
                       defaults: new { Controller = "Product", action = "List" });

                   routes.MapRoute(
                       name: "default",
                       template: "{controller=Product}/{action=List}/{id?}");
            });
            SeedData.EnsurePopulated(app);
            }  
    }
       
}

using BookStore.Shop.Api.Application.ShoppingFeatures.Commands;
using BookStore.Shop.Api.Persistence;
using BookStore.Shop.Api.RemoteServices.IServices;
using BookStore.Shop.Api.RemoteServices.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api
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

            services.AddControllers();
            services.AddDbContext<ContextShopping>(options =>
            {
                var mySqlConnString = Configuration.GetConnectionString("MySQLConnString");
                options.UseMySql(mySqlConnString, ServerVersion.AutoDetect(mySqlConnString));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Shop.Api", Version = "v1" });
            });
            services.AddHttpClient("Book", confg =>
            {
                confg.BaseAddress = new Uri(Configuration["Services:Books"]);
            });

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<BookServices>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddMediatR(typeof(CreateShoppingCommand).Assembly);
            services.AddScoped<IBookServices, BookServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore.Shop.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

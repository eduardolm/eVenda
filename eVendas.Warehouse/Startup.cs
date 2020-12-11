using AutoMapper;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Dto;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository;
using eVendas.Warehouse.Service;
using eVendas.Warehouse.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eVendas.Warehouse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string _dbUser;
        private string _dbPassword;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            _dbUser = Configuration["Connection:User"];
            _dbPassword = Configuration["Connection:Password"];

            services.AddMvcCore(options => options.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddDbContext<MainContext>(options => options
                .UseSqlServer($"Server=127.0.0.1,1433;Database=Warehouse;" +
                              $"User Id={_dbUser};Password={_dbPassword}"));
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IValidator<Product>, ProductValidator>();
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
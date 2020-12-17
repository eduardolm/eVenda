using AutoMapper;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Dto;
using eVendas.Warehouse.Helper;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository;
using eVendas.Warehouse.Repository.GenericRepository;
using eVendas.Warehouse.Service;
using eVendas.Warehouse.Service.GenericService;
using eVendas.Warehouse.Service.MessageHandlerFactory;
using eVendas.Warehouse.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace eVendas.Warehouse
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
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

            services.AddHostedService<BusListener>();
            services.AddMvcCore(options => options.EnableEndpointRouting = false);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.AddDbContext<MainContext>(options => options
                .UseSqlServer($"Server=127.0.0.1,1433;Database=Warehouse;" +
                              $"User Id={_dbUser};Password={_dbPassword}"));
            
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IValidator<Product>, ProductValidator>();
            services.AddScoped<IMessageFactory, MessageFactory>();
            services.AddScoped<IMessageHandler, MessageHandler>();
            services.AddScoped<IUpdateProduct, UpdateProduct>();
            
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
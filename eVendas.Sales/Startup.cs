using AutoMapper;
using eVendas.Sales.Context;
using eVendas.Sales.Dto;
using eVendas.Sales.Helper;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository;
using eVendas.Sales.Repository.GenericRepository;
using eVendas.Sales.Service;
using eVendas.Sales.Service.GenericService;
using eVendas.Sales.Service.MessageHandlerFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace eVendas.Sales
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

            services.AddMvcCore(options => options.EnableEndpointRouting = false);
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            
            services.AddDbContext<MainContext>(options => options
                .UseSqlServer($"Server=127.0.0.1,1433;Database=Sales;" +
                              $"User Id={_dbUser};Password={_dbPassword}"));
            
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IProductSaleRepository, ProductSaleRepository>();
            services.AddScoped<IProductSaleService, ProductSaleService>();
            services.AddScoped<IMessageHandler, MessageHandler>();
            services.AddScoped<IMessageFactory, MessageFactory>();
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
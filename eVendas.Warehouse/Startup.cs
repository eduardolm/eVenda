using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Repository;
using eVendas.Warehouse.Repository.GenericRepository;
using eVendas.Warehouse.Service.GenericService;
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
            
            services.AddControllers();
            services.AddDbContext<DbContext>(options => options
                .UseSqlServer($"Server=127.0.0.1,1433;Database=SupplierList;" +
                              $"User Id={_dbUser};Password={_dbPassword}"));
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IProductRepository, ProductRepository>();
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eVendas.Sales.Context
{
    public class MainContext : DbContext
    {
        private IConfiguration Configuration { get; }
        private string _connectionUser;
        private string _connectionPassword;

        public MainContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        
        // public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new ProductMapConfig());
            
            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _connectionUser = Configuration["Connection:User"];
            _connectionPassword = Configuration["Connection:Password"];
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer($"Server=127.0.0.1,1433;Database=Sales;" +
                                  $"User Id={_connectionUser};Password={_connectionPassword}");
            }
        }
    }
}
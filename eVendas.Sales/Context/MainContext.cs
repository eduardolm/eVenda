using eVendas.Sales.Mapping;
using eVendas.Sales.Model;
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
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapConfig());

            modelBuilder.Entity<Sale>()
                .Property(x => x.Total)
                .HasColumnType("decimal(6,2");

            modelBuilder.Entity<ProductSale>()
                .HasKey(x => new {x.ProductId, x.SaleId});
            
            modelBuilder.Entity<ProductSale>()
                .HasOne(ps => ps.Product)
                .WithMany(s => s.ProductSales)
                .HasForeignKey(ps => ps.ProductId);
            
            modelBuilder.Entity<ProductSale>()
                .HasOne(ps => ps.Sale)
                .WithMany(s => s.ProductSales)
                .HasForeignKey(ps => ps.SaleId);
            
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
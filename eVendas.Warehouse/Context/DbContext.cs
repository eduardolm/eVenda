using eVendas.Warehouse.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eVendas.Warehouse.Context
{
    public class Context : DbContext
    {
        private IConfiguration Configuration { get; }
        private string _connectionUser;
        private string _connectionPassword;

        public Context(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Product>()
                .Property(x => x.Sku)
                .HasColumnType("varchar")
                .HasColumnName("product_code")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .HasColumnType("varchar")
                .HasColumnName("nome")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.Price)
                .HasColumnType("decimal")
                .HasColumnName("preco")
                .HasPrecision(2)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.Quantity)
                .HasColumnType("int")
                .HasColumnName("quantidade")
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("data_cadastro")
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.UpdatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("data_atualizacao")
                .IsRequired();
            
            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _connectionUser = Configuration["Connection:User"];
            _connectionPassword = Configuration["Connection:Password"];
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer($"Server=127.0.0.1,1433;Database=Warehouse;" +
                                  $"User Id={_connectionUser};Password={_connectionPassword}");
            }
        }
    }
}
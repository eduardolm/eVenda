using eVendas.Warehouse.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVendas.Warehouse.Mapping
{
    public class ProductMapConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produtos")
                .HasKey(x => x.Id);

            builder.Property(x => x.Sku)
                .HasColumnType("varchar(20)")
                .HasColumnName("codigo")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasColumnName("nome")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal")
                .HasColumnName("preco")
                .HasPrecision(2)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasColumnType("int")
                .HasColumnName("quantidade")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("data_cadastro")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("data_atualizacao")
                .IsRequired();
        }
    }
}
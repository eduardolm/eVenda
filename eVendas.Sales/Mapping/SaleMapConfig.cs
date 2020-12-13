using eVendas.Sales.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVendas.Sales.Mapping
{
    public class SaleMapConfig :  IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Vendas")
                .HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .HasColumnType("int")
                .HasColumnName("produto_id")
                .IsRequired();
            
            builder.Property(x => x.Quantity)
                .HasColumnType("int")
                .HasColumnName("quantidade")
                .IsRequired();
            
            builder.Property(x => x.Total)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("total")
                .IsRequired();
            
            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("data_venda")
                .IsRequired();
            
            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2")
                .HasColumnName("data_alteracao")
                .IsRequired();
        }
    }
}
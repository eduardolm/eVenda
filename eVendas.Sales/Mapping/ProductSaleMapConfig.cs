using eVendas.Sales.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVendas.Sales.Mapping
{
    public class ProductSaleMapConfig : IEntityTypeConfiguration<ProductSale>
    {
        public void Configure(EntityTypeBuilder<ProductSale> builder)
        {
            builder.ToTable("Produtos_Vendas")
                .HasKey(x => new {x.ProductId, x.SaleId});

            builder.Property(x => x.ProductId)
                .HasColumnType("int")
                .HasColumnName("produto_id")
                .IsRequired();
            
            builder.Property(x => x.SaleId)
                .HasColumnType("int")
                .HasColumnName("venda_id")
                .IsRequired();
        }
    }
}
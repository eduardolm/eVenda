using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    [Table("Produtos_Vendas")]
    public class ProductSale : Base, IBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
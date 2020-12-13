using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    [Table("Produtos_Vendas")]
    public class ProductSale : IBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int Id { get; set; }
    }
}
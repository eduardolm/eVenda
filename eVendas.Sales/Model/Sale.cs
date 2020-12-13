using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    [Table("Vendas")]
    public class Sale : IBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual IList<ProductSale> ProductSales { get; set; }
    }
}
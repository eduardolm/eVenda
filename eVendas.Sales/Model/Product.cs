using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    [Table("Produtos")]
    public class Product : IBase
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public virtual IList<ProductSale> ProductSales { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    [Table("Produtos")]
    public class Product : Base, IBase
    {
        public Product() {}

        public Product(int id, string sku, string name, decimal price, int quantity, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Sku = sku;
            Name = name;
            Price = price;
            Quantity = quantity;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public virtual IList<ProductSale> ProductSales { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Warehouse.Interface;

namespace eVendas.Warehouse.Model
{
    [Table("produtos")]
    public class Product : Base, IBase
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // public int Id { get; set; }
    }
}
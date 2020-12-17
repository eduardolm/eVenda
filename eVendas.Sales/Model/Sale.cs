using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    [Table("Vendas")]
    public class Sale : Base, IBase
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
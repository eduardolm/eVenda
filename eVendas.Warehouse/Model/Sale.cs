using System;
using eVendas.Warehouse.Interface;

namespace eVendas.Warehouse.Model
{
    public class Sale : Base, IBase
    {
        public Sale(){}

        public Sale(int id, int productId, int quantity, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
using System;
using eVendas.Sales.Enum;

namespace eVendas.Sales.Model.MessageFactoryModel
{
    public class WarehouseInputMessage
    {
        public string MessageTitle { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public WarehouseInputMessage()
        {
        }

        public WarehouseInputMessage(MessageType messageType, Product product)
        {
            MessageTitle = messageType.ToString();
            ProductId = product.Id;
            Sku = product.Sku;
            Name = product.Name;
            Price = product.Price;
            Quantity = product.Quantity;
            CreatedAt = product.CreatedAt;
            UpdatedAt = product.UpdatedAt;
        }
    }
}
using System;
using eVendas.Warehouse.Enum;

namespace eVendas.Warehouse.Model.MessageFactoryModel
{
    public class SaleInputMessage
    {
        public string MessageTitle { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UpdatedSale UpdatedSale { get; set; }

        public SaleInputMessage()
        {
        }

        public SaleInputMessage(MessageType messageType, Sale sale)
        {
            MessageTitle = messageType.ToString();
            SaleId = sale.Id;
            ProductId = sale.ProductId;
            Quantity = sale.Quantity;
            CreatedAt = sale.CreatedAt;
            UpdatedAt = sale.UpdatedAt;
        }
    }
}
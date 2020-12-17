using System;
using eVendas.Sales.Enum;

namespace eVendas.Sales.Model.MessageFactoryModel
{
    public class SaleOutputMessage
    {
        public string MessageTitle { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UpdatedSale UpdatedSale { get; set; }

        public SaleOutputMessage(MessageType messageType, Sale sale, UpdatedSale updatedSale=null)
        {
            MessageTitle = messageType.ToString();
            SaleId = sale.Id;
            ProductId = sale.ProductId;
            Quantity = sale.Quantity;
            CreatedAt = sale.CreatedAt;
            UpdatedAt = sale.UpdatedAt;
            UpdatedSale = updatedSale;
        } 
    }
}
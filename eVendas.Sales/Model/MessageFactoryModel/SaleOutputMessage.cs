using System;
using eVendas.Sales.Enum;

namespace eVendas.Sales.Model.MessageFactoryModel
{
    public class SaleOutputMessage
    {
        private string MessageTitle { get; set; }
        private int SaleId { get; set; }
        private int ProductId { get; set; }
        private int Quantity { get; set; }
        private DateTime CreatedAt { get; set; }
        private DateTime UpdatedAt { get; set; }
        private UpdatedSale UpdatedSale { get; set; }

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
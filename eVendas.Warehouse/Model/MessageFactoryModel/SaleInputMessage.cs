using System;

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
    }
}
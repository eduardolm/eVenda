using System;
using eVendas.Sales.Enum;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;
using Xunit;

namespace eVendas.SaleTest.Model.MessageFactoryModel
{
    public class SaleOutputMessageTest
    {
        [Fact]
        public void Test_NotEqual_SaleOutputMessage_AllArgs_Constructor()
        {
            var sale = new Sale();
            sale.Id = 1;
            sale.ProductId = 2;
            sale.Total = 100M;
            sale.Quantity = 20;
            sale.CreatedAt = new DateTime(2020, 08, 23);
            sale.UpdatedAt = new DateTime(2020, 10, 15);
            var messageType = MessageType.SaleCreated;
            var updatedSale = new UpdatedSale(1, 2, 10, 20);
            var test = new SaleOutputMessage(messageType, sale, updatedSale);
            
            var sale1 = new Sale();
            sale1.Id = 1;
            sale1.ProductId = 2;
            sale1.Total = 100M;
            sale1.Quantity = 20;
            sale1.CreatedAt = new DateTime(2020, 08, 23);
            sale1.UpdatedAt = new DateTime(2020, 10, 15);
            var messageType1 = MessageType.SaleCreated;
            var updatedSale1 = new UpdatedSale(1, 2, 10, 20);
            var test1 = new SaleOutputMessage(messageType1, sale1, updatedSale1);
            
            Assert.NotEqual(test.GetHashCode(), test1.GetHashCode());
            Assert.NotEqual(test, test1);
        }

        [Fact]
        public void Test_SaleOutputMessage_Getter()
        {
            var sale = new Sale();
            sale.Id = 1;
            sale.ProductId = 2;
            sale.Total = 100M;
            sale.Quantity = 20;
            sale.CreatedAt = new DateTime(2020, 08, 23);
            sale.UpdatedAt = new DateTime(2020, 10, 15);
            var messageType = MessageType.SaleCreated;
            var updatedSale = new UpdatedSale(1, 2, 10, 20);
            var test = new SaleOutputMessage(messageType, sale, updatedSale);
            
            Assert.Equal(20, test.Quantity);
            Assert.Equal(2, test.ProductId);
            Assert.Equal(1, test.SaleId);
            Assert.Equal(new DateTime(2020, 08, 23), test.CreatedAt);
            Assert.Equal(new DateTime(2020, 10, 15), test.UpdatedAt);
            Assert.Equal(1, test.UpdatedSale.OldProductId);
            Assert.Equal(2, test.UpdatedSale.NewProductId);
            Assert.Equal(10, test.UpdatedSale.OldQuantity);
            Assert.Equal(20, test.UpdatedSale.NewQuantity);
            Assert.Equal("SaleCreated", test.MessageTitle);
        }
    }
}
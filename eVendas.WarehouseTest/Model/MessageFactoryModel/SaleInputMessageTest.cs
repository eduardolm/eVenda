using System;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;
using Xunit;

namespace eVendas.WarehouseTest.Model.MessageFactoryModel
{
    public class SaleInputMessageTest
    {
        [Fact]
        public void Test_NotEqual_SaleInputMessage_AllArgs_Constructor()
        {
            var sale = new Sale();
            sale.Id = 1;
            sale.ProductId = 2;
            sale.Quantity = 20;
            sale.CreatedAt = new DateTime(2020, 08, 23);
            sale.UpdatedAt = new DateTime(2020, 10, 15);
            var messageType = MessageType.ProductCreated;
            var updatedSale = new UpdatedSale(1, 2, 10, 20);
            var test = new SaleInputMessage(messageType, sale);
            
            var sale1 = new Sale();
            sale1.Id = 1;
            sale1.ProductId = 2;
            sale1.Quantity = 20;
            sale1.CreatedAt = new DateTime(2020, 08, 23);
            sale1.UpdatedAt = new DateTime(2020, 10, 15);
            var messageType1 = MessageType.ProductCreated;
            var updatedSale1 = new UpdatedSale(1, 2, 10, 20);
            var test1 = new SaleInputMessage(messageType1, sale1);
            
            Assert.NotEqual(test.GetHashCode(), test1.GetHashCode());
            Assert.NotEqual(test, test1);
            Assert.NotEqual(updatedSale, updatedSale1);
        }

        [Fact]
        public void Test_NotEqual_SaleInputMessage_NoArgs_Constructor()
        {
            var test1 = new SaleInputMessage();
            var test2 = new SaleInputMessage();
            
            Assert.NotEqual(test1.GetHashCode(), test2.GetHashCode());
            Assert.IsType<SaleInputMessage>(test1);
            Assert.IsType<SaleInputMessage>(test2);
        }

        [Fact]
        public void Test_SaleInputMessage_Getter()
        {
            var sale = new Sale();
            sale.Id = 1;
            sale.ProductId = 2;
            sale.Quantity = 20;
            sale.CreatedAt = new DateTime(2020, 08, 23);
            sale.UpdatedAt = new DateTime(2020, 10, 15);
            var messageType = MessageType.ProductCreated;
            var test = new SaleInputMessage(messageType, sale);
            
            Assert.Equal(20, test.Quantity);
            Assert.Equal(2, test.ProductId);
            Assert.Equal(1, test.SaleId);
            Assert.Equal(new DateTime(2020, 08, 23), test.CreatedAt);
            Assert.Equal(new DateTime(2020, 10, 15), test.UpdatedAt);
            Assert.Equal("ProductCreated", test.MessageTitle);
        }
        
        [Fact]
        public void Test_SaleInputMessage_Setter()
        {
            var sale = new Sale();
            sale.Id = 1;
            sale.ProductId = 2;
            sale.Quantity = 20;
            sale.CreatedAt = new DateTime(2020, 08, 23);
            sale.UpdatedAt = new DateTime(2020, 10, 15);
            var updatedSale = new UpdatedSale(1, 2, 10, 20);
            var messageType = MessageType.ProductCreated;
            var test = new SaleInputMessage(messageType, sale);

            test.MessageTitle = "SaleCreated";
            test.SaleId = 2;
            test.ProductId = 1;
            test.Quantity = 30;
            test.CreatedAt = DateTime.Today;
            test.UpdatedAt = DateTime.Today;
            test.UpdatedSale = updatedSale;
            test.UpdatedSale.OldProductId = 4;
            test.UpdatedSale.NewProductId = 3;
            test.UpdatedSale.OldQuantity = 20;
            test.UpdatedSale.NewQuantity = 50;

            Assert.Equal(30, test.Quantity);
            Assert.Equal(1, test.ProductId);
            Assert.Equal(2, test.SaleId);
            Assert.Equal(DateTime.Today, test.CreatedAt);
            Assert.Equal(DateTime.Today, test.UpdatedAt);
            Assert.Equal("SaleCreated", test.MessageTitle);
            Assert.Equal(4, test.UpdatedSale.OldProductId);
            Assert.Equal(3, test.UpdatedSale.NewProductId);
            Assert.Equal(20, test.UpdatedSale.OldQuantity);
            Assert.Equal(50, test.UpdatedSale.NewQuantity);
        }
    }
}
using System;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;
using eVendas.Sales.Enum;
using Moq;
using Xunit;

namespace eVendas.SaleTest.Model.MessageFactoryModel
{
    public class WarehouseInputMessageTest
    {
        [Fact]
        public void Test_NotEqual_WarehouseInputMessage_NoArgs_Constructor()
        {
            var test1 = new WarehouseInputMessage();
            var test2 = new WarehouseInputMessage();
            
            Assert.NotEqual(test1.GetHashCode(), test2.GetHashCode());
            Assert.Equal(test1.GetType(), test2.GetType());
            Assert.Equal(typeof(WarehouseInputMessage), test1.GetType());
        }

        [Fact]
        public void Test_Getter_Setter_WarehouseInputMessage_NoArgs_Constructor()
        {
            var messageType = "ProductCreated";
            var product = new Product("1000", "Product 1", 10M, 15, new DateTime(2019, 03, 10),
                new DateTime(2020, 7, 14));
            product.Id = 1;
            var test1 = new WarehouseInputMessage();

            test1.MessageTitle = messageType;
            test1.ProductId = product.Id;
            test1.Sku = product.Sku;
            test1.Name = product.Name;
            test1.Price = product.Price;
            test1.Quantity = product.Quantity;
            test1.CreatedAt = product.CreatedAt;
            test1.UpdatedAt = product.UpdatedAt;
            
            Assert.Equal("ProductCreated", test1.MessageTitle);
            Assert.Equal(1, test1.ProductId);
            Assert.Equal("1000", test1.Sku);
            Assert.Equal("Product 1", test1.Name);
            Assert.Equal(10M, test1.Price);
            Assert.Equal(15, test1.Quantity);
            Assert.Equal(new DateTime(2019, 03, 10), test1.CreatedAt);
            Assert.Equal(new DateTime(2020, 7, 14), test1.UpdatedAt);
        }

        [Fact]
        public void Test_NotEqual_WarehouseInputMessage_AllArgs_Constructor()
        {
            var messageType = MessageType.SaleCreated;
            var product = new Product("1000", "Product 1", 10M, 15, new DateTime(2019, 03, 10),
                new DateTime(2020, 7, 14));
            product.Id = 1;
            var test1 = new WarehouseInputMessage(messageType, product);
            
            Assert.Equal("SaleCreated", test1.MessageTitle);
            Assert.Equal(1, test1.ProductId);
            Assert.Equal("1000", test1.Sku);
            Assert.Equal("Product 1", test1.Name);
            Assert.Equal(10M, test1.Price);
            Assert.Equal(15, test1.Quantity);
            Assert.Equal(new DateTime(2019, 03, 10), test1.CreatedAt);
            Assert.Equal(new DateTime(2020, 7, 14), test1.UpdatedAt);
        }
        
       
    }
}
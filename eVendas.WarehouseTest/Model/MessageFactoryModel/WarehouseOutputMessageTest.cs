using System;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;
using Xunit;

namespace eVendas.WarehouseTest.Model.MessageFactoryModel
{
    public class WarehouseOutputMessageTest
    {
        [Fact]
        public void Test_NotEqual_WarehouseOutputMessage_NoArgs_Constructor()
        {
            var prod1 = new Product();
            prod1.Sku = "1000";
            prod1.Name = "Product 1";
            prod1.Price = 10M;
            prod1.Quantity = 15;
            prod1.CreatedAt = new DateTime(2019, 3, 10);
            prod1.UpdatedAt = new DateTime(2020, 7, 14);
            prod1.Id = 1;
            
            var prod2 = new Product();
            prod2.Sku = "1001";
            prod2.Name = "Product 2";
            prod2.Price = 10.44M;
            prod2.Quantity = 20;
            prod2.CreatedAt = new DateTime(2019, 3, 10);
            prod2.UpdatedAt = new DateTime(2020, 7, 14);
            
            var test1 = new WarehouseOutputMessage(MessageType.ProductCreated, prod1);
            var test2 = new WarehouseOutputMessage(MessageType.ProductCreated, prod2);
            
            Assert.NotEqual(test1.GetHashCode(), test2.GetHashCode());
            Assert.Equal(test1.GetType(), test2.GetType());
            Assert.Equal(typeof(WarehouseOutputMessage), test1.GetType());
            Assert.Equal("ProductCreated", test1.MessageTitle);
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
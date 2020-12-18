using System;
using eVendas.Warehouse.Enum;
using Xunit;

namespace eVendas.WarehouseTest.Enum
{
    public class MessageTypeTest
    {
        [Fact]
        public void Test_Enum_Types()
        {
            Assert.Equal(typeof(MessageType), MessageType.ProductCreated.GetType());
            Assert.Equal(0, Convert.ToInt32(MessageType.ProductCreated));
            Assert.Equal(1, Convert.ToInt32(MessageType.ProductUpdated));
            Assert.Equal(2, Convert.ToInt32(MessageType.ProductDeleted));
            Assert.Equal("ProductCreated", MessageType.ProductCreated.ToString());
            Assert.Equal("ProductUpdated", MessageType.ProductUpdated.ToString());
            Assert.Equal("ProductDeleted", MessageType.ProductDeleted.ToString());
        }
    }
}
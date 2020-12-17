using System;
using eVendas.Sales.Enum;
using Xunit;

namespace eVendas.SaleTest.Enum
{
    public class MessageTypeTest
    {
        [Fact]
        public void Test_Enum_Types()
        {
            Assert.Equal(typeof(MessageType), MessageType.SaleCancelled.GetType());
            Assert.Equal(0, Convert.ToInt32(MessageType.SaleCreated));
            Assert.Equal(1, Convert.ToInt32(MessageType.SaleUpdated));
            Assert.Equal(2, Convert.ToInt32(MessageType.SaleCancelled));
            Assert.Equal("SaleCreated", MessageType.SaleCreated.ToString());
            Assert.Equal("SaleUpdated", MessageType.SaleUpdated.ToString());
            Assert.Equal("SaleCancelled", MessageType.SaleCancelled.ToString());
        }
    }
}
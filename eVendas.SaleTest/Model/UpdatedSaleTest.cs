using eVendas.Sales.Model;
using Xunit;

namespace eVendas.SaleTest.Model
{
    public class UpdatedSaleTest
    {
        [Fact]
        public void Test_NotEqual_Updated_Sale_AllArgs_Constructor()
        {
            var sale1 = new UpdatedSale(1, 2, 10, 20);
            var sale2 = new UpdatedSale(1, 2, 10, 20);
            
            Assert.NotEqual(sale1.GetHashCode(), sale2.GetHashCode());
            Assert.Equal(1, sale1.OldProductId);
            Assert.Equal(2, sale1.NewProductId);
            Assert.Equal(10, sale2.OldQuantity);
            Assert.Equal(20, sale2.NewQuantity);
        }

        [Fact]
        public void Test_Updated_Sale_Getter()
        {
            var sale1 = new UpdatedSale(1, 2, 10, 20);
            
            Assert.Equal(1, sale1.OldProductId);
            Assert.Equal(2, sale1.NewProductId);
            Assert.Equal(10, sale1.OldQuantity);
            Assert.Equal(20, sale1.NewQuantity);
        }
    }
}
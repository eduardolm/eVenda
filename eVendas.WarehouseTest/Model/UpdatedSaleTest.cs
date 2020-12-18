using eVendas.Warehouse.Model;
using Xunit;

namespace eVendas.WarehouseTest.Model
{
    public class UpdatedSaleTest
    {
        [Theory]
        [InlineData(1, 2, 20, 10)]
        [InlineData(2, 3, 10, 20)]
        [InlineData(3, 4, 30, 10)]
        [InlineData(4, 5, 20, 30)]
        [InlineData(5, 1, 15, 10)]
        public void Test_UpdatedSale_AllArgs_Constructor(int oldProductId, int newProductId, int oldQuantity, int newQuantity)
        {
            var test = new UpdatedSale(oldProductId, newProductId, oldQuantity, newQuantity);
   
            Assert.Equal(oldProductId, test.OldProductId);
            Assert.Equal(newProductId, test.NewProductId);
            Assert.Equal(oldQuantity, test.OldQuantity);
            Assert.Equal(newQuantity, test.NewQuantity);
        }

        [Fact]
        public void Test_UpdatedSale_Getter_Setter_Warehouse()
        {
            var test1 = new UpdatedSale(1, 2, 10, 20);
            test1.NewQuantity = 30;
            test1.OldQuantity = 5;
            test1.OldProductId = 4;
            test1.NewProductId = 5;
            
            Assert.Equal(30, test1.NewQuantity);
            Assert.Equal(5, test1.OldQuantity);
            Assert.Equal(4, test1.OldProductId);
            Assert.Equal(5, test1.NewProductId);
        }
    }
}
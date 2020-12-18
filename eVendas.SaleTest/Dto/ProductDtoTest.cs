using eVendas.Sales.Dto;
using Xunit;

namespace eVendas.SaleTest.Dto
{
    public class ProductDtoTest
    {
        [Fact]
        public void Test_ProductDto_NotEqual_NoArgs_Constructor()
        {
            var prod1 = new ProductDto();
            var prod2 = new ProductDto();
            
            Assert.NotEqual(prod1.GetHashCode(), prod2.GetHashCode());
        }
        
        [Fact]
        public void Test_Getter_Setter_ProductDto_NotEqual_NoArgs_Constructor()
        {
            var prod1 = new ProductDto();
            var prod2 = new ProductDto();

            prod1.Sku = "1000";
            prod1.Name = "Product 1";
            prod1.Price = 10M;
            prod1.Quantity = 15;
            
            prod2.Sku = "1001";
            prod2.Name = "Product 2";
            prod2.Price = 10.55M;
            prod2.Quantity = 20;
            
            Assert.NotEqual(prod1, prod2);
            Assert.Equal("1000", prod1.Sku);
            Assert.Equal("Product 1", prod1.Name);
            Assert.Equal(10M, prod1.Price);
            Assert.Equal(15, prod1.Quantity);
            Assert.Equal("1001", prod2.Sku);
            Assert.Equal("Product 2", prod2.Name);
            Assert.Equal(10.55M, prod2.Price);
            Assert.Equal(20, prod2.Quantity);
        }
    }
}
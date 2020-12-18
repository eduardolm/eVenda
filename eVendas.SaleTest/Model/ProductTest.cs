using System;
using eVendas.Sales.Model;
using Xunit;

namespace eVendas.SaleTest.Model
{
    public class ProductTest
    {
        [Fact]
        public void Test_Product_NotEqual_NoArgs_Constructor()
        {
            var prod1 = new Product();
            var prod2 = new Product();
            
            Assert.NotEqual(prod1.GetHashCode(), prod2.GetHashCode());
        }

        [Fact]
        public void Test_Product_NotEqual_AllArgs_Constructor()
        {
            var prod1 = new Product("1000", "Product 1", 10M, 15, new DateTime(2019, 03, 10),
                new DateTime(2020, 7, 14));
            var prod2 = new Product("1001", "Product 2", 10.44M, 20, new DateTime(2019, 03, 10),
                new DateTime(2020, 7, 14));
            
            Assert.NotEqual(prod1, prod2);
            Assert.Equal(prod1.CreatedAt, prod2.CreatedAt);
        }

        [Fact]
        public void Test_Product_Getter_Setter()
        {
            var prod = new Product();
            prod.Id = 1;
            prod.Name = "Test Product";
            prod.Sku = "3021";
            prod.Price = 32.5M;
            prod.Quantity = 20;
            prod.CreatedAt = new DateTime(2020, 08, 23);
            prod.UpdatedAt = new DateTime(2020, 10, 15);
            
            Assert.Equal(1, prod.Id);
            Assert.Equal("Test Product", prod.Name);
            Assert.Equal("3021", prod.Sku);
            Assert.Equal(32.5M, prod.Price);
            Assert.Equal(20, prod.Quantity);
            Assert.NotEqual(DateTime.Now, prod.CreatedAt);
            Assert.Equal(new DateTime(2020,10,15), prod.UpdatedAt);

        }
    }
}
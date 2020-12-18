using System;
using eVendas.Warehouse.Model;
using Xunit;

namespace eVendas.WarehouseTest.Model
{
    public class SaleTest
    {
        [Fact]
        public void Test_Sale_NotEqual_NoArgs_Constructor_Warehouse()
        {
            var sale1 = new Sale();
            var sale2 = new Sale();
            
            Assert.NotEqual(sale1.GetHashCode(), sale2.GetHashCode());
        }
        
        [Fact]
        public void Test_Sale_NotEqual_AllArgs_Constructor_Warehouse()
        {
            var sale1 = new Sale(1, 1, 20, new DateTime(2020,8,23), new DateTime(2020,10,15));
            var sale2 = new Sale(1, 1, 20, new DateTime(2020,8,23), new DateTime(2020,10,15));
            
            Assert.NotEqual(sale1.GetHashCode(), sale2.GetHashCode());
        }

        [Fact]
        public void Test_Sale_Getter_Setter_Warehouse()
        {
            var sale = new Sale();
            sale.Id = 1;
            sale.ProductId = 2;
            sale.Quantity = 20;
            sale.CreatedAt = new DateTime(2020, 08, 23);
            sale.UpdatedAt = new DateTime(2020, 10, 15);
            
            Assert.Equal(1, sale.Id);
            Assert.Equal(2, sale.ProductId);
            Assert.Equal(20, sale.Quantity);
            Assert.NotEqual(DateTime.Now, sale.CreatedAt);
            Assert.Equal(new DateTime(2020,10,15), sale.UpdatedAt);

        }
    }
}
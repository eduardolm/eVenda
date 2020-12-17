using System;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Model;
using eVendas.Sales.Repository;
using eVendas.SaleTest.Context;
using Xunit;

namespace eVendas.SaleTest.Repository
{
    public class SaleRepositoryTest
    {
        [Fact]
        public void Test_GetAll_Products_Sale()
        {
            var fakeContext = new FakeContext("GetAll_Sale");
            fakeContext.FillWith<Sale>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var countSales = context.Sales.Count();
                var repository = new SaleRepository(context);
                
                Assert.Equal(countSales, repository.GetAll().Count());
                Assert.IsType<SaleRepository>(repository);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_GetById__Sale(int id)
        {
            var fakeContext = new FakeContext("GetById_Sale");
            fakeContext.FillWith<Sale>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var expected = fakeContext.GetFakeData<Sale>().Find(x => x.Id == id);
                var repository = new SaleRepository(context);
                var actual = repository.GetById(id);
                
                Assert.IsType<SaleRepository>(repository);
                Assert.IsType<Sale>(actual);
                Assert.Equal(expected.CreatedAt, actual.CreatedAt);
                Assert.Equal(expected.UpdatedAt, actual.UpdatedAt);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.ProductId, actual.ProductId);
                Assert.Equal(expected.Quantity, actual.Quantity);
                Assert.Equal(expected.Total, actual.Total);
            }
        }
        
        [Fact]
        public void Test_Create_Sale()
        {
            var fakeContext = new FakeContext("Create_Sale");
            fakeContext.FillWith<Sale>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var fakeSale = new Sale();
                fakeSale.ProductId = 1;
                fakeSale.Total = 1826.40M;
                fakeSale.Quantity = 100;
                fakeSale.CreatedAt = new DateTime(2020, 02, 22);
                fakeSale.UpdatedAt = new DateTime(2020, 08, 15);
                
                var repository = new SaleRepository(context);
                repository.Create(fakeSale);
                var createdSale = repository.GetById(6);

                Assert.IsType<Sale>(createdSale);
                Assert.Equal(6, repository.GetAll().Count());
                Assert.NotEqual(0, createdSale.Id);
                Assert.Equal(1826.40M, createdSale.Total);
                Assert.Equal(100, createdSale.Quantity);
                Assert.Equal(new DateTime(2020, 02, 22), createdSale.CreatedAt);
                Assert.Equal(new DateTime(2020, 08, 15), createdSale.UpdatedAt);
                Assert.Equal(6, createdSale.Id);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_Update_Sale(int id)
        {
            var fakeContext = new FakeContext("Update_Sale");
            fakeContext.FillWith<Sale>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var currentSale = repository.GetById(id);

                currentSale.Quantity = 200;
                repository.Update(id, currentSale);
                
                Assert.Equal(200, repository.GetById(id).Quantity);
            }
        }
        
        [Fact]
        public void Test_Delete_Sale()
        {
            var fakeContext = new FakeContext("Delete_Sale");
            fakeContext.FillWith<Sale>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var currentCount = context.Sales.Count();
                
                Assert.Equal(5, currentCount);
                
                repository.Delete(1);
                
                Assert.Equal(4, context.Sales.Count());
            }
        }
    }
}
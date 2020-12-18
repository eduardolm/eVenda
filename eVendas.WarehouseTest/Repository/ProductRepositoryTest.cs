using System;
using System.Linq;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository;
using eVendas.WarehouseTest.Context;
using Xunit;

namespace eVendas.WarehouseTest.Repository
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void Test_GetAll_Products_Warehouse()
        {
            var fakeContext = new FakeContext("GetAll_Products_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var countProducts = context.Products.Count();
                var repository = new ProductRepository(context);
                
                Assert.Equal(countProducts, repository.GetAll().Count());
                Assert.IsType<ProductRepository>(repository);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_GetById_Products_Warehouse(int id)
        {
            var fakeContext = new FakeContext("GetByIdProducts_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var expected = fakeContext.GetFakeData<Product>().Find(x => x.Id == id);
                var repository = new ProductRepository(context);
                var actual = repository.GetById(id);
                
                Assert.IsType<ProductRepository>(repository);
                Assert.IsType<Product>(actual);
                Assert.Equal(expected.CreatedAt, actual.CreatedAt);
                Assert.Equal(expected.UpdatedAt, actual.UpdatedAt);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Sku, actual.Sku);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Price, actual.Price);
                Assert.Equal(expected.Quantity, actual.Quantity);
            }
        }
        
        [Fact]
        public void Test_Create_Product_Warehouse()
        {
            var fakeContext = new FakeContext("Create_Products_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var fakeProduct = new Product();
                fakeProduct.Sku = "3000";
                fakeProduct.Name = "Product test";
                fakeProduct.Price = 120M;
                fakeProduct.Quantity = 100;
                fakeProduct.CreatedAt = new DateTime(2020, 02, 22);
                fakeProduct.UpdatedAt = new DateTime(2020, 08, 15);
                
                var repository = new ProductRepository(context);
                repository.Create(fakeProduct);
                var createdProduct = repository.GetById(6);

                Assert.IsType<Product>(createdProduct);
                Assert.NotEqual(0, createdProduct.Id);
                Assert.Equal("Product test", createdProduct.Name);
                Assert.Equal("3000", createdProduct.Sku);
                Assert.Equal(120M, createdProduct.Price);
                Assert.Equal(100, createdProduct.Quantity);
                Assert.Equal(new DateTime(2020, 02, 22), createdProduct.CreatedAt);
                Assert.Equal(new DateTime(2020, 08, 15), createdProduct.UpdatedAt);
                Assert.Equal(6, createdProduct.Id);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_Update_Product_Warehouse(int id)
        {
            var fakeContext = new FakeContext("Update_Products_Warehosue");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var currentProduct = repository.GetById(id);

                currentProduct.Name = "123abc";
                repository.Update(id, currentProduct);
                
                Assert.Equal("123abc", repository.GetById(id).Name);
            }
        }
        
        [Fact]
        public void Test_Delete_Product_Warehouse()
        {
            var fakeContext = new FakeContext("Delete_Products_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var currentCount = context.Products.Count();
                
                Assert.Equal(5, currentCount);
                
                repository.Delete(1);
                
                Assert.Equal(4, context.Products.Count());
            }
        }
    }
}
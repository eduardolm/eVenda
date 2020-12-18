using System;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Model;
using eVendas.Sales.Repository;
using eVendas.Sales.Service;
using eVendas.SaleTest.Context;
using Xunit;

namespace eVendas.SaleTest.Service
{
    public class ProductServiceTest
    {
        [Fact]
        public void Test_GetAll_Products_Sale_Service()
        {
            var fakeContext = new FakeContext("GetAllProducts_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var countProducts = context.Products.Count();
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                
                Assert.Equal(countProducts, service.GetAll().Count());
                Assert.IsType<ProductRepository>(repository);
                Assert.IsType<ProductService>(service);
            }
        }
        
        /**
         * Tests if GetAll method returns only products with quantity greater than zero.
         */
        [Fact]
        public void Test_GetAll_Products_NonZero_Quantity_Sale_Service()
        {
            var fakeContext = new FakeContext("GetAllProducts_NonZero_Quantity_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var testProduct = new Product("1000", "Product 1", 10M, 0, new DateTime(2019, 03, 10),
                    new DateTime(2020, 7, 14));
                
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                var edu = service.Create(testProduct);
                
                Assert.Equal(6, context.Products.Count());
                Assert.Equal(5, service.GetAll().Count());
                Assert.IsType<ProductRepository>(repository);
                Assert.IsType<ProductService>(service);
                foreach (var item in service.GetAll())
                {
                    Assert.NotEqual(0, item.Quantity);
                }
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_GetById_Products_Sale_Service(int id)
        {
            var fakeContext = new FakeContext("GetByIdProducts_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var expected = fakeContext.GetFakeData<Product>().Find(x => x.Id == id);
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                var actual = service.GetById(id);
                
                Assert.IsType<ProductRepository>(repository);
                Assert.IsType<ProductService>(service);
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
        public void Test_Create_Product_Sale_Service()
        {
            var fakeContext = new FakeContext("Create_Products_Sale_Service");
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
                var service = new ProductService(repository);
                service.Create(fakeProduct);
                var createdProduct = service.GetById(6);

                Assert.IsType<Product>(createdProduct);
                Assert.Equal(6, repository.GetAll().Count());
                Assert.Equal(6, service.GetAll().Count());
                Assert.NotEqual(0, createdProduct.Id);
                Assert.Equal("Product test", createdProduct.Name);
                Assert.Equal("3000", createdProduct.Sku);
                Assert.Equal(120M, createdProduct.Price);
                Assert.Equal(100, createdProduct.Quantity);
                Assert.Equal(6, createdProduct.Id);
            }
        }
        
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_Update_Product_Sale_Service(int id)
        {
            var fakeContext = new FakeContext("Update_Products_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                var currentProduct = service.GetById(id);

                currentProduct.Name = "123abc";
                service.Update(id, currentProduct);
                
                Assert.Equal("123abc", service.GetById(id).Name);
            }
        }

        [Fact]
        public void Test_Return_Null_When_Id_NotFound()
        {
            var fakeContext = new FakeContext("Return_Null_When_Id_NotFound_Products_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                var currentProduct = new Product("1000", "Product 1", 10M, 0, new DateTime(2019, 03, 10),
                    new DateTime(2020, 7, 14));
                
                Assert.Null(service.GetById(6));
                Assert.Null(service.GetById(-10));
                Assert.Null(service.GetById(0));
            }
        }
        
        [Fact]
        public void Test_Return_Message_When_Id_NotFound()
        {
            var fakeContext = new FakeContext("Return_Message_When_Id_NotFoun_Products_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                var currentProduct = new Product("1000", "Product 1", 10M, 0, new DateTime(2019, 03, 10),
                    new DateTime(2020, 7, 14));

                var response = service.Update(6, currentProduct);
                
                Assert.Equal("{ Message = Produto não encontrado. }", response.ToString());
            }
        }
        
        [Fact]
        public void Test_Delete_Product_Sale_Service()
        {
            var fakeContext = new FakeContext("Delete_Product_Sale_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var service = new ProductService(repository);
                var currentCount = service.GetAll().Count();
                
                Assert.Equal(5, currentCount);
                
                service.Delete(1);
                
                Assert.Equal(4, context.Products.Count());
            }
        }
    }
}
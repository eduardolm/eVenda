using System;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Model;
using eVendas.Sales.Repository.GenericRepository;
using eVendas.Sales.Service.GenericService;
using eVendas.SaleTest.Context;
using Xunit;

namespace eVendas.SaleTest.Service
{
    public class GenericServiceTest
    {
        [Fact]
        public void Generic_GetAll_Service()
        {
            var fakeContext = new FakeContext("Generic_GetAll_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var contextProducts = context.Products;
                var serviceProducts = service.GetAll();
                var contextCount = contextProducts.Count();
                var serviceCount = serviceProducts.Count();
                
                Assert.Equal(contextCount, serviceCount);
                Assert.IsType<int>(serviceCount);
                Assert.Equal(contextProducts, serviceProducts);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Generic_GetById_Service(int id)
        {
            var fakeContext = new FakeContext("Generic_GetById_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var contextProduct = context.Products.Find(id);
                var serviceProduct = service.GetById(id);

                Assert.IsType<Product>(serviceProduct);
                Assert.Equal(contextProduct, serviceProduct);
            }
        }
        
        [Fact]
        public void Generic_Create_Service()
        {
            var fakeContext = new FakeContext("Generic_Create_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var product = new Product("1000", "Product 1", 10M, 15, new DateTime(2019, 03, 10),
                    new DateTime(2020, 7, 14));

                var response = service.Create(product);
                
                Assert.Equal(6, service.GetAll().Count());
                Assert.Equal("{ Message = Produto cadastrado com sucesso. }", response.ToString());
                Assert.Equal("1000", service.GetById(6).Sku);
                Assert.Equal("Product 1", service.GetById(6).Name);
                Assert.Equal(10M, service.GetById(6).Price);
                Assert.Equal(15, service.GetById(6).Quantity);
                Assert.Equal(2019, service.GetById(6).CreatedAt.Year);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Generic_Update_Service(int id)
        {
            var fakeContext = new FakeContext("Generic_Update_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var contextProduct = context.Products.Find(id);
                contextProduct.Quantity = 150;
                var response = service.Update(id, contextProduct);

                Assert.Equal("{ Message = Produto alterado com sucesso. }", response.ToString());
                Assert.Equal(150, service.GetById(id).Quantity);
            }
        }
        
        [Fact]
        public void Generic_Update_Product_NotFound_Service()
        {
            var fakeContext = new FakeContext("Generic_Update_Product_NotFound_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var contextProduct = context.Products.Find(1);
                contextProduct.Quantity = 150;
                var response = service.Update(6, contextProduct);

                Assert.Equal("{ Message = Produto não encontrado. }", response.ToString());
            }
        }
        
        [Fact]
        public void Generic_Delete_Product_NotFound_Service()
        {
            var fakeContext = new FakeContext("Generic_Delete_Product_NotFound_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var countBefore = service.GetAll().Count();
                
                Assert.Equal(5, countBefore);
                var response = service.Delete(6);
                Assert.Equal("{ Message = Produto não encontrado. }", response.ToString());
                Assert.Equal(5, service.GetAll().Count());
            }
        }
        
        [Fact]
        public void Generic_Delete_Service()
        {
            var fakeContext = new FakeContext("Generic_Delete_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var countBefore = service.GetAll().Count();
                
                Assert.Equal(5, countBefore);
                var response = service.Delete(1);
                Assert.Equal("{ Message = Produto removido com sucesso. }", response.ToString());
                Assert.Equal(4, service.GetAll().Count());
            }
        }
    }
}
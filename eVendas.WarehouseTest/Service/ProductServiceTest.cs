using System;
using System.Linq;
using System.Threading.Tasks;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository;
using eVendas.Warehouse.Service;
using eVendas.WarehouseTest.Context;
using Moq;
using Xunit;
using Sale = eVendas.Sales.Model.Sale;
using UpdatedSale = eVendas.Sales.Model.UpdatedSale;

namespace eVendas.WarehouseTest.Service
{
    public class ProductServiceTest
    {
        [Fact]
        public void Test_GetAll_Products_Warehouse_Service()
        {
            var fakeContext = new FakeContext("GetAllProducts_Warehouse_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var countProducts = context.Products.Count();
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);

                Assert.Equal(countProducts, service.GetAll().Count());
                Assert.IsType<ProductRepository>(repository);
                Assert.IsType<ProductService>(service);
            }
        }

        /**
         * Tests if GetAll method returns only products with quantity greater than zero.
         */
        [Fact]
        public void Test_GetAllProducts_NonZero_Quantity_Warehouse_Service()
        {
            var fakeContext = new FakeContext("GetAllProducts_NonZero_Quantity_Warehouse_Service");
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
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);

                var edu = service.Create(fakeProduct);

                Assert.Equal(6, context.Products.Count());
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
        public void Test_GetById_Products_Warehouse_Service(int id)
        {
            var fakeContext = new FakeContext("GetByIdProducts_Warehouse_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var expected = fakeContext.GetFakeData<Product>().Find(x => x.Id == id);
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);
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
        public void Test_Create_Product_Warehouse_Service()
        {
            var fakeContext = new FakeContext("Test_Create_Products_Warehouse_Service");
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
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);
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
        public void Test_Update_Product_Warehouse_Service(int id)
        {
            var fakeContext = new FakeContext("Update_Products_Warehouse_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);

                var currentProduct = service.GetById(id);
                currentProduct.Name = "123abc";
                service.Update(id, currentProduct);

                Assert.Equal("123abc", service.GetById(id).Name);
            }
        }

        [Fact]
        public void Test_Return_Null_When_Id_NotFound_Warehouse()
        {
            var fakeContext = new FakeContext("Return_Null_When_Id_NotFound_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);
                var currentProduct = service.GetById(1);

                Assert.Null(service.GetById(6));
                Assert.Null(service.GetById(-10));
                Assert.Null(service.GetById(0));
            }
        }

        [Fact]
        public void Test_Return_Message_When_Id_NotFound_Warehouse()
        {
            var fakeContext = new FakeContext("Return_Message_When_Id_NotFound_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);
                var currentProduct = service.GetById(1);

                var response = service.Update(6, currentProduct);

                Assert.Equal("{ Message = Produto não encontrado. }", response.ToString());
            }
        }

        [Fact]
        public void Test_Delete_Product_Warehouse_Service()
        {
            var fakeContext = new FakeContext("Delete_Product_Warehouse_Service");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);
                var currentCount = service.GetAll().Count();

                Assert.Equal(5, currentCount);

                service.Delete(1);

                Assert.Equal(4, context.Products.Count());
            }
        }
    }
}
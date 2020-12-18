using System.Collections.Generic;
using System.Threading.Tasks;
using eVendas.Sales.Context;
using eVendas.Sales.Controllers;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository;
using eVendas.Sales.Service;
using eVendas.SaleTest.Context;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eVendas.SaleTest.Controller
{
    public class SaleControllerTest
    {
        [Fact]
        public void Test_SaleController_GetAll()
        {
            var fakeContext = new FakeContext("SaleController_GetAll");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var response = controller.GetAll();
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal(repository.GetAll(), okResult.Value);
                Assert.IsType<List<Sale>>(resultValue);
            }
        }
        
        [Fact]
        public void Test_Return_Message_When_NotFound_SaleController_GetAll()
        {
            var fakeContext = new FakeContext("Return_Message_When_NotFound_SaleController_GetAll");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var response = controller.GetAll();
                var okResult = response as NotFoundObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(404, okResult.StatusCode);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_SaleController_GetById(int id)
        {
            var fakeContext = new FakeContext("SaleController_GetById");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var response = controller.GetById(id);
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal(repository.GetById(id), okResult.Value);
                Assert.IsType<Sale>(resultValue);
            }
        }
        
        [Fact]
        public void Test_Return_Message_NotFound_SaleController_GetById()
        {
            var fakeContext = new FakeContext("Return_Message_NotFound_SaleController_GetById");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var response = controller.GetById(6);
                var okResult = response as NotFoundObjectResult;
                
                Assert.NotNull(okResult);
                Assert.Equal(404, okResult.StatusCode);
            }
        }
        
        [Fact]
        public async void Test_SaleController_Create()
        {
            var fakeContext = new FakeContext("Test_SaleController_Create");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var sale = new Sale();
                sale.ProductId = 1;
                sale.Quantity = 40;
                var response = await controller.Create(sale);
                var okResult = response as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
            }
        }
        
        [Fact]
        public async void Test_Return_Message_NotFound_SaleController_Create()
        {
            var fakeContext = new FakeContext("Test_Return_Message_NotFound_SaleController_Create");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var sale = new Sale();
                sale.ProductId = 10;
                sale.Quantity = 40;
                var response = await controller.Create(sale);
                var okResult = response as NotFoundObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(404, okResult.StatusCode);
            }
        }
        
        [Fact]
        public async void Test_Return_Message_BadRequest_SaleController_Create()
        {
            var fakeContext = new FakeContext("Test_Return_Message_BadRequest_SaleController_Create");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var sale = new Sale();
                sale.ProductId = 1;
                sale.Quantity = 400;
                var response = await controller.Create(sale);
                var okResult = response as BadRequestObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(400, okResult.StatusCode);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async void Test_SaleController_Update(int id)
        {
            var fakeContext = new FakeContext("Test_SaleController_Update");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var sale = service.GetById(id); ;
                sale.Quantity = 40;
                var response = await controller.Update(id, sale);
                var okResult = response as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
            }
        }
        
        [Fact]
        public async void Test_Return_Message_When_NotFound_SaleController_Update()
        {
            var fakeContext = new FakeContext("Return_Message_When_NotFound_SaleController_Update");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);
                var sale = new Sale();
                sale.ProductId = 1;
                sale.Quantity = 40;
                var response = await controller.Update(6, sale);
                var okResult = response as NotFoundObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(404, okResult.StatusCode);
            }
        }

        [Fact]
        public async void Test_SaleController_Delete()
        {
            var fakeContext = new FakeContext("Test_SaleController_Delete");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);

                var response = await controller.Delete(1);
                var okResult = response as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
            }
        }
        
        [Fact]
        public async void Test_Return_Message_When_NotFound_SaleController_Delete()
        {
            var fakeContext = new FakeContext("Test_Return_Message_When_NotFound_SaleController_Delete");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new SaleRepository(context);
                var productRepository = new ProductRepository(context);
                var updateMock = new Mock<IUpdateProduct>();
                updateMock
                    .Setup(x => x.UpdateStock(It.IsAny<Sale>(), It.IsAny<Sale>()));

                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Sale>(), It.IsAny<UpdatedSale>()))
                    .Returns(Task.CompletedTask);

                var service = new SaleService(repository, messageMock.Object, updateMock.Object, productRepository);
                var controller = new SaleController(service);

                var response = await controller.Delete(10);
                var okResult = response as NotFoundObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(404, okResult.StatusCode);
            }
        }
    }
}
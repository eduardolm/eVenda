using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Mappers;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Controller;
using eVendas.Warehouse.Dto;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository;
using eVendas.Warehouse.Service;
using eVendas.WarehouseTest.Context;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eVendas.WarehouseTest.Controller
{
    public class ProductControllerTest
    {
        [Fact]
        public void Test_ProductController_GetAll_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductController_GetAll_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDto, Product>();
                    cfg.CreateMap<Product, ProductDto>();
                });
                var mapper = config.CreateMapper();

                var validator = new Mock<IValidator<Product>>();
                validator.Setup(x => x.Validate(It.IsAny<Product>())).Returns(new ValidationResult());

                var service = new ProductService(repository, messageMock.Object);
                var controller = new ProductController(service, mapper, validator.Object);
                var response = controller.GetAll();
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal(repository.GetAll(), okResult.Value);
                Assert.IsType<List<Product>>(resultValue);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_ProductController_GetByID_Warehouse(int id)
        {
            var fakeContext = new FakeContext("Test_ProductController_GetByID_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDto, Product>();
                    cfg.CreateMap<Product, ProductDto>();
                });
                var mapper = config.CreateMapper();

                var validator = new Mock<IValidator<Product>>();
                validator.Setup(x => x.Validate(It.IsAny<Product>())).Returns(new ValidationResult());

                var service = new ProductService(repository, messageMock.Object);
                var controller = new ProductController(service, mapper, validator.Object);
                var response = controller.GetById(id);
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal(repository.GetById(id), okResult.Value);
            }
        }
        
        [Fact]
        public void Test_ProductController_Create_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductController_Create_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDto, Product>();
                    cfg.CreateMap<Product, ProductDto>();
                });
                var mapper = config.CreateMapper();

                var validator = new Mock<IValidator<Product>>();
                validator.Setup(x => x.Validate(It.IsAny<Product>())).Returns(new ValidationResult());

                var service = new ProductService(repository, messageMock.Object);
                var controller = new ProductController(service, mapper, validator.Object);
                var productDto = new ProductDto();
                productDto.Sku = "1000";
                productDto.Name = "Product 1";
                productDto.Price = 68.80M;
                productDto.Quantity = 100;
                var product = mapper.Map<ProductDto, Product>(productDto);

                var response = controller.Create(productDto);
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_ProductController_Update_Warehouse(int id)
        {
            var fakeContext = new FakeContext("Test_ProductController_Update_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDto, Product>();
                    cfg.CreateMap<Product, ProductDto>();
                });
                var mapper = config.CreateMapper();

                var validator = new Mock<IValidator<Product>>();
                validator.Setup(x => x.Validate(It.IsAny<Product>())).Returns(new ValidationResult());

                var service = new ProductService(repository, messageMock.Object);
                var controller = new ProductController(service, mapper, validator.Object);
                
                var productDto = new ProductDto();
                productDto.Sku = "1000";
                productDto.Name = "Product 1";
                productDto.Price = 68.80M;
                productDto.Quantity = 100;
                
                var response = controller.Update(id, productDto);
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
            }
        }
        
        [Fact]
        public void Test_ProductController_Delete_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductController_Delete_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new ProductRepository(context);
                
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDto, Product>();
                    cfg.CreateMap<Product, ProductDto>();
                });
                var mapper = config.CreateMapper();

                var validator = new Mock<IValidator<Product>>();
                validator.Setup(x => x.Validate(It.IsAny<Product>())).Returns(new ValidationResult());

                var service = new ProductService(repository, messageMock.Object);
                var controller = new ProductController(service, mapper, validator.Object);

                var countBefore = service.GetAll().Count();
                var response = controller.Delete(1);
                var countAfter = service.GetAll().Count();
                var okResult = response as OkObjectResult;
                var resultValue = okResult.Value;
                
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);
                Assert.Equal(5, countBefore);
                Assert.Equal(4, countAfter);
            }
        }
    }
}
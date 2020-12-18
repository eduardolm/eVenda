using System.Threading.Tasks;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository;
using eVendas.Warehouse.Service;
using eVendas.Warehouse.Validator;
using eVendas.WarehouseTest.Context;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace eVendas.WarehouseTest.Validator
{
    public class ProductValidatorTest
    {
        private ProductValidator validator;

        [Fact]
        public void Test_ProductValidator_NameNull_Warehouse()
        {
            var fakeContext = new FakeContext("ProductValidator_Name_Null_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Price = 18M;
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("Nome não pode ser deixado em branco.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Name_TooShort_Warehouse()
        {
            var fakeContext = new FakeContext("ProductValidator_Name_TooShort_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Name = "A";
                product.Price = 18M;
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("O nome deve ter entre 3 e 50 caracteres.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Name_TooLong_Warehouse()
        {
            var fakeContext = new FakeContext("ProductValidator_Name_TooLong_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Name = "Teste de validação de nome com mais que 50 caracteres. Isso é bem difícil de fazer.";
                product.Price = 18M;
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("O nome deve ter entre 3 e 50 caracteres.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_SkuNull_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_SkuNull_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Name = "Product 1";
                product.Price = 18M;
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("Código não pode ser deixado em branco.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Sku_TooShort_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_Sku_TooShort_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "AB";
                product.Name = "Product 1";
                product.Price = 18M;
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("O código deve ter entre 3 e 20 caracteres.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Price_Null_or_Blank_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_Price_Blank_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Name = "Product 1";
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("Preço não pode ser deixado em branco.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Price_Smaller_Than_Zero_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_Price_Smaller_Than_Zero_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Name = "Product 1";
                product.Price = -15M;
                product.Quantity = 100;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("Preço deve obrigatoriamente ser maior que zero.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Quantity_Blank_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_Quantity_Blank_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Name = "Product 1";
                product.Price = 15M;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("Quantidade não pode ser deixado em branco.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Quantity_Smaller_Than_Zero_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_Quantity_Smaller_Than_Zero_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);

                var product = new Product();
                product.Sku = "1000";
                product.Name = "Product 1";
                product.Price = 15M;
                product.Quantity = -15;
                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("A quantidade informada deve ser maior ou igual a zero.", result.Errors[0].ErrorMessage);
            }
        }
        
        [Fact]
        public void Test_ProductValidator_Same_Name_Or_Sku_Warehouse()
        {
            var fakeContext = new FakeContext("Test_ProductValidator_Same_Name_Or_Sku_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                validator = new ProductValidator(context);
                var repository = new ProductRepository(context);
                var messageMock = new Mock<IMessageHandler>();
                messageMock
                    .Setup(x => x
                        .SendMessageAsync(It.IsAny<MessageType>(), It.IsAny<Product>()))
                    .Returns(Task.CompletedTask);
                var service = new ProductService(repository, messageMock.Object);

                var product = new Product();
                product.Sku = "1001";
                product.Name = "Product 1";
                product.Price = 15M;
                product.Quantity = 150;
                
                service.Create(product);

                var result = validator.TestValidate(product);

                result.ShouldHaveAnyValidationError();
                Assert.Equal("Produto já cadastrado.", result.Errors[0].ErrorMessage);
            }
        }
    }
}
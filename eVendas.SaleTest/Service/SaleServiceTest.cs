using System.Linq;
using System.Threading.Tasks;
using eVendas.Sales.Context;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository;
using eVendas.Sales.Service;
using eVendas.SaleTest.Context;
using Moq;
using Xunit;

namespace eVendas.SaleTest.Service
{
    public class SaleServiceTest
    {
        [Fact]
        public void Test_GetAll_Sale_Service()
        {
            var fakeContext = new FakeContext("GetAll_Sale_Service");
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
                var test = service.GetAll();
                var countSales = context.Sales.Count();
                
                Assert.Equal(countSales, test.Count());
                Assert.Equal(context.Sales.ToList(), test);
                Assert.IsType<SaleRepository>(repository);
                Assert.IsType<ProductRepository>(productRepository);
                Assert.IsType<Mock<IUpdateProduct>>(updateMock);
                Assert.IsType<Mock<IMessageHandler>>(messageMock);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_GetById_Sale_Service(int id)
        {
            var fakeContext = new FakeContext("GetById_Sale_Service");
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
                var test = service.GetById(id);
                
                Assert.Equal(id, test.Id);
                Assert.NotNull(test);
                Assert.IsType<Sale>(test);
                Assert.Equal(repository.GetById(id).Quantity, service.GetById(id).Quantity);
            }
        }
        
        [Fact]
        public async void Test_Create_Sale_Service()
        {
            var fakeContext = new FakeContext("Create_Sale_Service");
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
                var sale = new Sale();
                sale.ProductId = 1;
                sale.Quantity = 10;
                var response = await service.Create(sale);
                var newSale = (from s in repository.GetAll()
                    where s.Quantity == 10
                    where s.ProductId == 1
                    select s).FirstOrDefault();

                Assert.IsType<Sale>(newSale);
                Assert.Equal("{ Message = Venda efetuada com sucesso. }", response.ToString());
                Assert.Equal(6, newSale.Id);
                Assert.Equal(688.80M, newSale.Total);
                Assert.Equal(2020, newSale.CreatedAt.Year);
            }
        }
        
        [Fact]
        public void Test_Return_Message_When_Product_NotFound_Create_Sale_Service()
        {
            var fakeContext = new FakeContext("Return_Message_When_Product_NotFound_Create_Sale_Service");
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
                var sale = new Sale();
                var newSale = service.Create(sale);
                
                Assert.Equal("{ Message = Produto não encontrado. }", newSale.Result.ToString());
            }
        }
        
        [Fact]
        public async void Test_Return_Message_When_Quantity_Larger_Than_Stock_Create_Sale_Service()
        {
            var fakeContext = new FakeContext("Return_Message_When_Quantity_Larger_Than_Stock_Create_Sale_Service");
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
                var sale = new Sale();
                sale.ProductId = 1;
                sale.Quantity = 300;
                var newSale = await service.Create(sale);
                
                Assert.Equal("{ Message = Quantidade indisponível no estoque. }", newSale.ToString());
            }
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async void Test_Update_Sale_Service(int id)
        {
            var fakeContext = new FakeContext("Update_Sale_Service");
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
                var currentSale = service.GetById(id);
                currentSale.Quantity = 20;
                var response = await service.Update(id, currentSale);
                var newSale = service.GetById(id);
                
                Assert.Equal("{ Message = Venda alterada com sucesso. }", response.ToString());
                Assert.Equal(20, newSale.Quantity);
            }
        }
        
        [Fact]
        public async void Test_Return_Message_When_Wrong_Id_Update_Sale_Service()
        {
            var fakeContext = new FakeContext("Return_Message_When_Wrong_Id_Update_Sale_Service");
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
                var currentSale = service.GetById(1);
                var response = await service.Update(2, currentSale);
                
                Assert.Equal("{ Message = Não é possível alterar o produto vendido. " +
                             "É preciso cancelar a venda e criar uma nova venda. }", response.ToString());
                
            }
        }
        
        [Fact]
        public async void Test_Return_Message_When_Wrong_Id_Delete_Sale_Service()
        {
            var fakeContext = new FakeContext("Return_Message_When_Wrong_Id_Delete_Sale_Service");
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
                var response = await service.Delete(6);
                
                Assert.Null(response);
            }
        }
        
        [Fact]
        public async void Test_Delete_Sale_Service()
        {
            var fakeContext = new FakeContext("Delete_Sale_Service");
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
                var response = await service.Delete(1);
                
                Assert.Equal("{ Message = Venda cancelada com sucesso. }", response.ToString());
                
            }
        }
    }
}
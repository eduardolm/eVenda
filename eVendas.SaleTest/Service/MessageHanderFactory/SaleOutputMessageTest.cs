using System;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Enum;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;
using eVendas.Sales.Repository.GenericRepository;
using eVendas.Sales.Service.GenericService;
using eVendas.Sales.Service.MessageHandlerFactory;
using eVendas.SaleTest.Context;
using Xunit;

namespace eVendas.SaleTest.Service.MessageHanderFactory
{
    public class SaleOutputMessageTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_Create_SaleOutputMessage(int id)
        {
            var msg = new MessageFactory();
            var fakeContext = new FakeContext("Generic_Delete_Product_NotFound_Service");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Sale>(context);
                var service = new GenericService<Sale>(repository);

                var sale = service.GetById(id);

                var response = msg.Create(MessageType.SaleCreated, sale);

                Assert.IsType<SaleOutputMessage>(response);
                Assert.Equal("SaleCreated", response.MessageTitle);
            }
        }
    }
}
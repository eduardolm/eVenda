
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;
using eVendas.Warehouse.Repository.GenericRepository;
using eVendas.Warehouse.Service.GenericService;
using eVendas.Warehouse.Service.MessageHandlerFactory;
using eVendas.WarehouseTest.Context;
using Xunit;

namespace eVendas.WarehouseTest.Service.MessageHandlerFactory
{
    public class SaleOutputMessageTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_Create_WarehouseOutputMessage_Warehouse(int id)
        {
            var msg = new MessageFactory();
            var fakeContext = new FakeContext("Create_WarehouseOutputMessage_Warehouse");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var product = service.GetById(id);

                var response = msg.Create(MessageType.ProductCreated, product);

                Assert.IsType<WarehouseOutputMessage>(response);
                Assert.Equal("ProductCreated", response.MessageTitle);
            }
        }
    }
}
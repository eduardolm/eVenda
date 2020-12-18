using eVendas.Warehouse.Context;
using eVendas.Warehouse.Helper;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository.GenericRepository;
using eVendas.Warehouse.Service.GenericService;
using eVendas.WarehouseTest.Context;
using Xunit;

namespace eVendas.WarehouseTest.Helper
{
    public class UtilitiesTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public static void Test_Convert_To_Bytes_And_From_Bytes(int id)
        {
            var fakeContext = new FakeContext("Convert_To_Bytes_product");
            fakeContext.FillWith<Product>();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var repository = new GenericRepository<Product>(context);
                var service = new GenericService<Product>(repository);

                var product = service.GetById(id);
                var byteProduct = product.ToJsonBytes();
                var productFromByte = byteProduct.ParseJson<Product>();

                Assert.IsType<byte[]>(byteProduct);
                Assert.IsType<Product>(productFromByte);
                Assert.Equal(product.CreatedAt, productFromByte.CreatedAt);
                Assert.Equal(product.UpdatedAt, productFromByte.UpdatedAt);
                Assert.Equal(product.Name, productFromByte.Name);
                Assert.Equal(product.Sku, productFromByte.Sku);
            }
        }

        [Fact]
        public void Test_Return_Null_When_Object_Null()
        {
            Product product = null;

            var byteProduct = product.ToJsonBytes();
            var productFromByte = byteProduct.ParseJson<Product>();
            
            Assert.Null(byteProduct);
            Assert.Null(productFromByte);
        }
    }
}
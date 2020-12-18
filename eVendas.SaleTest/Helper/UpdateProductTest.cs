using eVendas.Sales.Context;
using eVendas.Sales.Helper;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository;
using eVendas.SaleTest.Context;
using Moq;
using Xunit;

namespace eVendas.SaleTest.Helper
{
    public class UpdateProductTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_UpdateStock(int id)
        {
            var fakeContext = new FakeContext("UpdateProduct_Helper");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var productRepository = new ProductRepository(context);
                var repository = new SaleRepository(context);
                var productService = new Mock<IProductService>();
                productService.Setup(x => x.GetById(It.IsAny<int>())).Returns(productRepository.GetById(id));
                productService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Product>()))
                    .Returns<int, Product>((productId, product) => "{ Message = Produto alterado com sucesso. }");
                var update = new UpdateProduct(productService.Object);

                var oldSale = repository.GetById(id);
                var newSale = new Sale();
                newSale.ProductId = id;
                newSale.Quantity = 15;
                update.UpdateStock(newSale, oldSale);
                
                Assert.Equal(115, productRepository.GetById(id).Quantity);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test_CancelSale_Helper(int id)
        {
            var fakeContext = new FakeContext("CancelSale_Helpe");
            fakeContext.FillWithAll();

            using (var context = new MainContext(fakeContext.FakeOptions, fakeContext.FakeConfiguration().Object))
            {
                var productRepository = new ProductRepository(context);
                var repository = new SaleRepository(context);
                var productService = new Mock<IProductService>();
                productService.Setup(x => x.GetById(It.IsAny<int>())).Returns(productRepository.GetById(id));
                productService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Product>()))
                    .Returns<int, Product>((productId, product) => "{ Message = Produto alterado com sucesso. }");
                var update = new UpdateProduct(productService.Object);

                var sale = repository.GetById(id);
                
                Assert.Equal(100, productRepository.GetById(id).Quantity);
                update.CancelSale(sale);
                Assert.Equal(130, productRepository.GetById(id).Quantity);
            }
        }
    }
}
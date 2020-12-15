using System.Diagnostics;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;

namespace eVendas.Sales.Helper
{
    public class UpdateProduct : IUpdateProduct
    {
        private readonly IProductService _productService;

        public UpdateProduct(IProductService productService)
        {
            _productService = productService;
        }
        
        public void UpdateStock(Sale newSale, Sale oldSale=null)
        {
            var quantity = 0;
            if (oldSale != null) quantity = oldSale.Quantity;
            
            var product = _productService.GetById(newSale.ProductId);

            if (newSale.Quantity > quantity)
            {
                product.Quantity -= (newSale.Quantity - quantity);
                _productService.Update(newSale.ProductId, product);
            }

            if (newSale.Quantity < quantity)
            {
                product.Quantity += (quantity - newSale.Quantity);
                _productService.Update(newSale.ProductId, product);
            }
        }

        public void CancelSale(Sale sale)
        {
            var product = _productService.GetById(sale.ProductId);
            product.Quantity += sale.Quantity;
            _productService.Update(sale.ProductId, product);
        }
    }
}
using System;
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
            var product = _productService.GetById(newSale.ProductId);
            product.UpdatedAt = DateTime.Now;
            
            if (oldSale != null)
            {
                if (newSale.Quantity != oldSale.Quantity)
                {
                    product.Quantity += oldSale.Quantity;
                    _productService.Update(product.Id, product);
                    product.Quantity -= newSale.Quantity;
                    _productService.Update(product.Id, product);
                }
 
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
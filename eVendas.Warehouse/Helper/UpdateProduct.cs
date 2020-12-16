using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;

namespace eVendas.Warehouse.Helper
{
    public class UpdateProduct : IUpdateProduct
    {
        private readonly IProductService _productService;

        public UpdateProduct(IProductService productService)
        {
            _productService = productService;
        }
        
        public void UpdateItem(Product product, Sale sale, SaleInputMessage receivedMessage)
        {
            switch (receivedMessage.MessageTitle)
            {
                case "SaleCreated":
                {
                    product.Quantity -= sale.Quantity;
                    _productService.Update(sale.ProductId, product);
                    break;
                }
                case "SaleUpdated":
                {
                    if (receivedMessage.UpdatedSale.OldProductId != receivedMessage.UpdatedSale.NewProductId)
                    {
                        product.Quantity += receivedMessage.UpdatedSale.OldQuantity;
                        _productService.Update(product.Id, product);
                        break;
                    }

                    if (receivedMessage.UpdatedSale.OldQuantity != receivedMessage.UpdatedSale.NewQuantity)
                    {
                        product.Quantity += receivedMessage.UpdatedSale.OldQuantity;
                        _productService.Update(product.Id, product);
                        product.Quantity -= receivedMessage.UpdatedSale.NewQuantity;
                        _productService.Update(product.Id, product);
                    }
                    break;
                }
                case "SaleCancelled":
                {
                    product.Quantity += receivedMessage.UpdatedSale.OldQuantity;
                    _productService.Update(product.Id, product);
                    break;
                }
            }
        }
    }
}
using System.Collections.Generic;
using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface IProductSaleService : IGenericService<ProductSale>
    {
        ProductSale GetById(int productId, int saleId);
    }
}
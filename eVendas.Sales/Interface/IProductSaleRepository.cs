using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface IProductSaleRepository : IGenericRepository<ProductSale>
    {
        ProductSale GetById(int productId, int saleId);
        void Update(int productId, int saleId, ProductSale productSale);
        void Delete(int productId, int saleId);
    }
}
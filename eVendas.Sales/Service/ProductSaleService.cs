using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Service.GenericService;

namespace eVendas.Sales.Service
{
    public class ProductSaleService : GenericService<ProductSale>, IProductSaleService
    {
        private readonly IProductSaleRepository _repository;
        
        public ProductSaleService(IProductSaleRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ProductSale GetById(int productId, int saleId)
        {
            if (productId > 0 && saleId > 0)
                return _repository.GetById(productId, saleId);
            return null;
        }
    }
}
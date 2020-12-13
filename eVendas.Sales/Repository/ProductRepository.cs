using eVendas.Sales.Context;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository.GenericRepository;

namespace eVendas.Sales.Repository 
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MainContext context) : base(context)
        {
        }
    }
}
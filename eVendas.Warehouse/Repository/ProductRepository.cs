using eVendas.Warehouse.Context;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository.GenericRepository;

namespace eVendas.Warehouse.Repository 
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MainContext context) : base(context)
        {
        }
    }
}
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Repository
{
    public class ProductRepository : GenericRepository<Product>, IGenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
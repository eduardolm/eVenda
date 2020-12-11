using System.Collections.Generic;
using System.Linq;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MainContext _mainContext;
        public ProductRepository(DbContext context, MainContext mainContext) : base(context)
        {
            _mainContext = mainContext;
        }

        public new IEnumerable<Product> GetAll()
        {
            return from p in _mainContext.Products.ToList() 
                where p.Quantity > 0 
                select p;
        }

        public new Product GetById(int id)
        {
            return (from p in _mainContext.Products.ToList()
                where p.Id == id
                where p.Quantity > 0
                select p).FirstOrDefault();
        }
    }
}
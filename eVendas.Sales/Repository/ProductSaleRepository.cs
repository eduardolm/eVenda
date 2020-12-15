using System;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Sales.Repository
{
    public class ProductSaleRepository : GenericRepository<ProductSale>, IProductSaleRepository
    {
        private readonly MainContext _context;
        private readonly DbSet<ProductSale> _dbSet;
        
        public ProductSaleRepository(MainContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<ProductSale>();
        }

        public ProductSale GetById(int productId, int saleId)
        {
            return _dbSet.Find(productId, saleId);
        }

        public void Update(int productId, int saleId, ProductSale entity)
        {
            if (_dbSet.Find(productId, saleId) != null)
            {
                DetachLocal(_ => _.Id == entity.ProductId);
                DetachLocal(_ => _.Id == entity.SaleId);
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(int productId, int saleId)
        {
            var entity =  _dbSet.Find(productId, saleId);

            if (entity != null)

                _dbSet.Remove(entity);
        }
        
        public new void Dispose()
        {
            _context.Dispose();
        }
        
        public override void DetachLocal(Func<ProductSale, bool> predicate)
        {
            var local = _context.Set<ProductSale>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Repository
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly MainContext _context;
        public ProductRepository(MainContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return from p in _context.Products.ToList() 
                where p.Quantity > 0 
                select p;
        }

        public Product GetById(int id)
        {
            return (from p in _context.Products.ToList()
                where p.Id == id
                where p.Quantity > 0
                select p).FirstOrDefault();
        }

        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Product entity)
        {
            DetachLocal(_ => _.Id == entity.Id);
            _context.Products.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Products.FirstOrDefault(x => x.Id == id);
            
            if (entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public void DetachLocal(Func<Product, bool> predicate)
        {
            var local = _context.Products.Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
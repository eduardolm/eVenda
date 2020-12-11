using System;
using System.Collections.Generic;
using System.Linq;
using eVendas.Warehouse.Interface;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBase
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            DetachLocal(_ => _.Id == entity.Id);
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public virtual void DetachLocal(Func<T, bool> predicate)
        {
            var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
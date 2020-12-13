using System.Collections.Generic;
using System.Linq;
using eVendas.Sales.Context;
using eVendas.Sales.Interface;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Sales.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBase
    {
        private readonly MainContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MainContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return  _dbSet.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if (! _dbSet.AsNoTracking().Any(x => x.Id.Equals(entity.Id))) return null;

            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public T Delete(int id)
        {
            var entity =  _dbSet.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));

            if (entity == null) return null;

            _dbSet.Remove(entity);
            return entity;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
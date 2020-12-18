using System;
using System.Collections.Generic;
using System.Linq;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using Microsoft.EntityFrameworkCore;

namespace eVendas.Warehouse.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBase
    {
        private readonly MainContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MainContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return  _dbSet.Find(id);
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id, T entity)
        {
            if (_dbSet.Find(id) != null)
            {
                DetachLocal(_ => _.Id == entity.Id);
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entity =  _dbSet.Find(id);

            if (entity != null)

            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
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
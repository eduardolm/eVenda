using System;
using System.Collections.Generic;
using eVendas.Warehouse.Model;

namespace eVendas.Warehouse.Interface
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
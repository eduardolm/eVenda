using System;
using System.Collections.Generic;

namespace eVendas.Sales.Interface
{
    public interface IGenericService<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        object Create(T entity);
        object Update(int id, T entity);
        object Delete(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVendas.Warehouse.Interface
{
    public interface IGenericService<T> : IDisposable where T : class, IBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        object Create(T product);
        object Update(int id, T entity);
        object Delete(int id);
    }
}
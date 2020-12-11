using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVendas.Warehouse.Interface
{
    public interface IGenericRepository<T> : IDisposable where T : class, IBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
using System.Collections.Generic;

namespace eVendas.Warehouse.Interface
{
    public interface IGenericRepository<T> where T : class, IBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        T Delete(int id);
        void Dispose();
    }
}
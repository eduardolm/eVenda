using System.Collections.Generic;
using eVendas.Warehouse.Interface;

namespace eVendas.Warehouse.Service.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : class, IBase
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public object Create(T entity)
        {
            return  _repository.Create(entity);
        }

        public object Update(int id, T entity)
        {
            return _repository.Update(entity);
        }

        public object Delete(int id)
        {
            return  _repository.Delete(id);
        }
        
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using FluentValidation;

namespace eVendas.Sales.Service.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : Base
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
            _repository.Create(entity);
            return new {Message = "Produto cadastrado com sucesso."};
        }

        public object Update(int id, T entity)
        {
            if (id > 0 && _repository.GetById(id) != null)
            {
                _repository.Update(id, entity);
                return new {Message = "Produto alterado com sucesso."};
            }

            return new {Message = "Produto não encontrado."};
        }

        public object Delete(int id)
        {
            if (id > 0 && _repository.GetById(id) != null)
            {
                _repository.Delete(id);
                return new {Message = "Produto removido com sucesso."};
            }

            return new {Message = "Produto não encontrado."};
        }
    }
}
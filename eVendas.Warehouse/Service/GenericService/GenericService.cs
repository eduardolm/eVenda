using System.Collections.Generic;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using FluentValidation;

namespace eVendas.Warehouse.Service.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : Base
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IValidator<T> _validator;

        public GenericService(IGenericRepository<T> repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
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
            var result = _validator.Validate(entity);

            if (result.IsValid)
            {
                _repository.Create(entity);
                return new {Message = "Produto cadastrado com sucesso."};
            }

            return new {Message = "Ocorreu um erro ao processar sua solicitação. Verifique os dados inseridos."};
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
        
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
using System.Collections.Generic;
using Castle.Core.Internal;
using eVendas.Warehouse.Interface;
using FluentValidation;

namespace eVendas.Warehouse.Service.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : class, IBase
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
            if (_repository.ToString().IsNullOrEmpty() || id < 0)
                return null;
            return _repository.GetById(id);
        }

        public object Create(T entity)
        {
            var result = _validator.Validate(entity);

            if (result.IsValid)
            {
                _repository.Create(entity);
                return new {Message = "Product created successfully."};
            }
            return null;
        }

        public object Update(int id, T entity)
        {
            if (!entity.Id.ToString().IsNullOrEmpty() || entity.Id > 0 || _repository.GetById(entity.Id) != null)
            {
                _repository.Update(entity);
                return new {Message = "Product updated successfully."};
            }
            return null;
        }

        public object Delete(int id)
        {
            if (!id.ToString().IsNullOrEmpty() || id > 0 || _repository.GetById(id) != null)
            {
                _repository.Delete(id);
                return new {Message = "Product deleted successfully."};
            }
            return null;
        }
        
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
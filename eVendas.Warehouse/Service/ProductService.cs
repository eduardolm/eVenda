using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using FluentValidation;

namespace eVendas.Warehouse.Service
{
    public class ProductService :  IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<Product> _validator;

        public ProductService(IProductRepository repository, IValidator<Product> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public Product GetById(int id)
        {
            if (id > 0 && _repository.GetById(id) != null)
                return _repository.GetById(id);
            return null;
        }

        public object Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            
            var result = _validator.Validate(product);
            
            if (result.IsValid)
            {
                _repository.Create(product);
                return new {Message = "Produto adicionado com sucesso."};
            }
            return null;
        }

        public object Update(int id, Product product)
        {
            if (id > 0 && _repository.GetById(id) != null)
            {
                var productToUpdate = _repository.GetById(id);

                if (productToUpdate != null)
                {
                    product.CreatedAt = productToUpdate.CreatedAt;
                    product.UpdatedAt = DateTime.Now;
                    product.Id = id;
                    _repository.Update(product);
                    return new {Message = "Produto alterado com sucesso."};
                }
            }
            return null;
        }

        public object Delete(int id)
        {
            if (!id.ToString().IsNullOrEmpty() || id > 0 || _repository.GetById(id) != null)
            {
                _repository.Delete(id);
                return new {Message = "Produto apagado."};
            }
            return null;
        }
        
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
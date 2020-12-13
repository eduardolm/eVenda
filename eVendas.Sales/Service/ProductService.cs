using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;

namespace eVendas.Sales.Service
{
    public class ProductService :  IProductService
    {
        private readonly IProductRepository _repository;
        
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<Product> GetAll()
        {
            // TODO: Alterar response para devolver todos os produtos com estoque = 0 ou não
            var response = (from p in  _repository.GetAll()
                where p.Quantity > 0
                select p);

            return response;
        }
        
        public Product GetById(int id)
        {
            if (id > 0 &&  _repository.GetById(id) != null)
            {
                // TODO: Alterar response para devolver todos os produtos com estoque = 0 ou não
                var response = (from p in  _repository.GetAll()
                    where p.Quantity > 0
                    where p.Id.Equals(id)
                    select p).FirstOrDefault();

                return response;
            }
            return null;
        }
        
        public object Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _repository.Create(product);
            return new {Message = "Produto adicionado com sucesso."};
        }

        public object Update(int id, Product product)
        {
            if (id > 0 && _repository.GetById(id) != null)
            {
                var productToUpdate =  _repository.GetById(id);
        
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
                return new {Message = "Produto removido com sucesso."};
            }
            return null;
        }
        
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
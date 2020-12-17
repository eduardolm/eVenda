using System;
using System.Collections.Generic;
using System.Linq;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Service.GenericService;

namespace eVendas.Sales.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        
        public ProductService(IGenericRepository<Product> repository) : base(repository)
        {
            _repository = repository;
        }
        
        public new IEnumerable<Product> GetAll()
        {
            var response = (from p in  _repository.GetAll()
                where p.Quantity > 0
                select p);

            return response;
        }
        
        public new Product GetById(int id)
        {
            if (id > 0 &&  _repository.GetById(id) != null)
            {
                var response = (from p in  _repository.GetAll()
                    where p.Quantity > 0
                    where p.Id.Equals(id)
                    select p).FirstOrDefault();

                return response;
            }
            return null;
        }
        
        public new object Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _repository.Create(product);
            return new {Message = "Produto adicionado com sucesso."};
        }

        public new object Update(int id, Product product)
        {
            if (id > 0 && _repository.GetById(id) != null)
            {
                var productToUpdate =  _repository.GetById(id);
        
                if (productToUpdate != null)
                {
                    product.CreatedAt = productToUpdate.CreatedAt;
                    product.UpdatedAt = DateTime.Now;
                    product.Id = id;
                     _repository.Update(id, product);
                    return new {Message = "Produto alterado com sucesso."};
                }
            }
            return new {Message = "Produto não encontrado."};
        }
    }
}
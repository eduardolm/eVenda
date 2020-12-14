using System;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Service.GenericService;
using FluentValidation;

namespace eVendas.Warehouse.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IValidator<Product> _validator;
        private readonly IGenericRepository<Product> _productRepository;
        
        public ProductService(
            IGenericRepository<Product> repository, 
            IValidator<Product> validator) : base(repository, validator)
        {
            _validator = validator;
            _productRepository = repository;
        }

        public new object Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            var result = _validator.Validate(product);

            if (result.IsValid)
            {
                _productRepository.Create(product);
                return new {Message = "Produto cadastrado com sucesso."};
            }

            return new
            {
                Message = "Ocorreu um erro ao cadastrar o produto. Verifique os dados informados e tente novamente."
            };
        }

        public new object Update(int id, Product product)
        {
            if (id > 0 && _productRepository.GetById(id) != null)
            {
                var productToUpdate = _productRepository.GetById(id);

                if (productToUpdate != null)
                {
                    product.CreatedAt = productToUpdate.CreatedAt;
                    product.UpdatedAt = DateTime.Now;
                    product.Id = id;
                    
                    _productRepository.Update(id, product);
                    return new {Message = "Produto alterado com sucesso."};
                }
            }

            return new {Message = "Produto não encontrado."};
        }
    }
}
using System;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Service.GenericService;

namespace eVendas.Warehouse.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMessageHandler _messageHandler;
        
        public ProductService(
            IGenericRepository<Product> repository, IMessageHandler messageHandler) : base(repository)
        {
            _productRepository = repository;
            _messageHandler = messageHandler;
        }

        public new object Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            _productRepository.Create(product);
            _messageHandler.SendMessageAsync(MessageType.ProductCreated, product);
            return new {Message = "Produto cadastrado com sucesso."};
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
                    _messageHandler.SendMessageAsync(MessageType.ProductUpdated, product);
                    return new {Message = "Produto alterado com sucesso."};
                }
            }

            return new {Message = "Produto não encontrado."};
        }
        
        public new object Delete(int id)
        {
            if (id > 0 && _productRepository.GetById(id) != null)
            {
                var product = _productRepository.GetById(id);
                _productRepository.Delete(id);
                _messageHandler.SendMessageAsync(MessageType.ProductDeleted, product);
                return new {Message = "Produto removido com sucesso."};
            }
        
            return new {Message = "Produto não encontrado."};
        }
    }
}
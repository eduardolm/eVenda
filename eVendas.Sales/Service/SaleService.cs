using System;
using System.Threading.Tasks;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Service.GenericService;

namespace eVendas.Sales.Service
{
    public class SaleService : GenericService<Sale>, ISaleService
    {
        private readonly IGenericRepository<Sale> _repository;
        private readonly IUpdateProduct _updateProduct;
        private readonly IMessageHandler _messageHandler;
        private readonly IProductRepository _productRepository;

        public SaleService(
            IGenericRepository<Sale> repository, IMessageHandler messageHandler, IUpdateProduct updateProduct,
            IProductRepository productRepository) : base(repository)
        {
            _repository = repository;
            _messageHandler = messageHandler;
            _updateProduct = updateProduct;
            _productRepository = productRepository;
        }

        public new async Task<object> Create(Sale sale)
        {
            var product = _productRepository.GetById(sale.ProductId);

            if (product == null) return new {Message = "Produto não encontrado."};
            
            if (sale.Quantity > product.Quantity) return new {Message = "Quantidade indisponível no estoque."};
            
            if (sale.Equals(null)) return null;
            sale.CreatedAt = DateTime.Now;
            sale.UpdatedAt = DateTime.Now;
            _repository.Create(sale);
            _updateProduct.UpdateStock(sale);
            await _messageHandler.SendMessageAsync(MessageType.SaleCreated, sale);

            return new {Message = "Venda efetuada com sucesso."};
        }

        public new async Task<object> Update(int id, Sale sale)
        {
            if (id <= 0 || _repository.GetById(id) == null) return null;

            var product = _productRepository.GetById(sale.ProductId);
            var saleToUpdate = _repository.GetById(id);
            var updatedSale = new UpdatedSale(
                saleToUpdate.ProductId, 
                sale.ProductId, 
                saleToUpdate.Quantity, 
                sale.Quantity);
            
            sale.CreatedAt = saleToUpdate.CreatedAt;
            sale.UpdatedAt = DateTime.Now;
            sale.Id = id;

            _repository.Update(id, sale);
            await _messageHandler.SendMessageAsync(MessageType.SaleUpdated, sale, updatedSale);

            if (updatedSale.OldProductId != updatedSale.NewProductId)
            {
                _updateProduct.UpdateStock(sale, saleToUpdate);
            }

            if (updatedSale.OldQuantity > updatedSale.NewQuantity)
            {
                _updateProduct.UpdateStock(sale, saleToUpdate);
            }
            
            if (updatedSale.OldQuantity < updatedSale.NewQuantity)
            {
                if (updatedSale.NewQuantity > product.Quantity) return null;
                _updateProduct.UpdateStock(sale, saleToUpdate);
            }

            return new {Message = "Venda alterada com sucesso."};
        }

        public new async Task<object> Delete(int id)
        {
            if (id <= 0 || _repository.GetById(id) == null) return null;
            var saleToDelete = _repository.GetById(id);
            
            if (saleToDelete == null) return null;
            _repository.Delete(id);
            await _messageHandler.SendMessageAsync(MessageType.SaleCancelled, saleToDelete);
            _updateProduct.CancelSale(saleToDelete);

            return new {Message = "Venda cancelada com sucesso."};
        }
    }
}
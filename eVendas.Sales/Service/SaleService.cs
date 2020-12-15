using System;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Service.GenericService;

namespace eVendas.Sales.Service
{
    public class SaleService : GenericService<Sale>, ISaleService
    {
        private readonly IGenericRepository<Sale> _repository;
        private readonly IProductSaleRepository _productSaleRepository;
        private readonly IUpdateProduct _updateProduct;
        private readonly IMessageHandler _messageHandler;

        public SaleService(
            IGenericRepository<Sale> repository, IMessageHandler messageHandler, 
            IProductSaleRepository productSaleRepository, IUpdateProduct updateProduct) : base(repository)
        {
            _repository = repository;
            _messageHandler = messageHandler;
            _productSaleRepository = productSaleRepository;
            _updateProduct = updateProduct;
        }

        public new object Create(Sale sale)
        {
            if (sale == null) return null;
            sale.CreatedAt = DateTime.Now;
            sale.UpdatedAt = DateTime.Now;
            _repository.Create(sale);
            _updateProduct.UpdateStock(sale);
            _messageHandler.SendMessageAsync(MessageType.SaleCreated, sale);

            var productSale = new ProductSale {ProductId = sale.ProductId, SaleId = sale.Id};
            _productSaleRepository.Create(productSale);
            
            return new {Message = "Venda efetuada com sucesso."};
        }

        public new object Update(int id, Sale sale)
        {
            if (id <= 0 || _repository.GetById(id) == null) return null;
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
            _messageHandler.SendMessageAsync(MessageType.SaleUpdated, sale, updatedSale);

            if (updatedSale.OldProductId != updatedSale.NewProductId)
            {
                _updateProduct.UpdateStock(sale, saleToUpdate);
            }

            if (updatedSale.OldQuantity > updatedSale.NewQuantity)
            {
                _updateProduct.UpdateStock(sale, saleToUpdate);
            }

            var productSale = new ProductSale {ProductId = sale.ProductId, SaleId = sale.Id};
            _productSaleRepository.Update(productSale.ProductId, productSale.SaleId, productSale);
            
            return new {Message = "Venda alterado com sucesso."};
        }

        public new object Delete(int id)
        {
            if (id <= 0 || _repository.GetById(id) == null) return null;
            var saleToDelete = _repository.GetById(id);
            
            if (saleToDelete == null) return null;
            _repository.Delete(id);
            _messageHandler.SendMessageAsync(MessageType.SaleCancelled, saleToDelete);
            
            var productSale = new ProductSale();
            productSale.ProductId = saleToDelete.ProductId;
            productSale.SaleId = saleToDelete.Id;
            _productSaleRepository.Delete(productSale.ProductId, productSale.SaleId);
            _updateProduct.CancelSale(saleToDelete);

            return new {Message = "Venda cancelada com sucesso."};
        }
    }
}
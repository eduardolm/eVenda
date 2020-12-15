using System;
using System.Threading;
using System.Threading.Tasks;
using eVendas.Sales.Helper;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;
using Microsoft.Azure.ServiceBus;

namespace eVendas.Warehouse.Helper
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IMessageFactory _factory;
        private readonly IProductService _productService;

        public MessageHandler(IMessageFactory factory, IProductService productService)
        {
            _factory = factory;
            _productService = productService;
        }
        public Task SendMessageAsync(MessageType messageType, Product product)
        {
            var messageToSend = _factory.Create(messageType, product);

            var serviceBusClient = new TopicClient("Endpoint=sb://evenda-service-bus.servicebus.windows.net/;SharedAccessKeyName=SendOnly;SharedAccessKey=SQvgDluEkyote1s6huIX+J/kUv7XdgiE6PFrmMF+5Ik=", "vendarealizada");

            var message = new Message(messageToSend.ToJsonBytes());
            message.ContentType = "application/json";
            message.UserProperties.Add("CorrelationId", Guid.NewGuid().ToString());

            serviceBusClient.SendAsync(message);

            return Task.CompletedTask;
        }

        public Task ReceiveMessageAsync()
        {
            var serviceBusClient = 
                new SubscriptionClient("Endpoint=sb://evenda-service-bus.servicebus.windows.net/;SharedAccessKeyName=ListenOnly;SharedAccessKey=d4ggwX0BK5c9zrS1yEWduJF/ac560r4SFw5so9551k8=", "vendarealizada", "VendaRealizadaEstoque");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            serviceBusClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }
        
        public Task ProcessMessageAsync(Message message, CancellationToken arg2)
        {
            var receivedMessage = message.Body.ParseJson<SaleInputMessage>();
            var sale = new Sale(receivedMessage.SaleId, receivedMessage.ProductId, receivedMessage.Quantity, 
                receivedMessage.CreatedAt, receivedMessage.UpdatedAt);
            
            var product = _productService.GetById(receivedMessage.ProductId);

            switch (receivedMessage.MessageTitle)
            {
                case "SaleCreated":
                {
                    product.Quantity -= sale.Quantity;
                    _productService.Update(sale.ProductId, product);
                    break;
                }
                case "SaleUpdated":
                {
                    if (receivedMessage.UpdatedSale.OldProductId != receivedMessage.UpdatedSale.NewProductId)
                    {
                        product.Quantity += receivedMessage.UpdatedSale.OldQuantity;
                        _productService.Update(product.Id, product);
                        break;
                    }

                    if (receivedMessage.UpdatedSale.OldQuantity != receivedMessage.UpdatedSale.NewQuantity)
                    {
                        product.Quantity += receivedMessage.UpdatedSale.OldQuantity;
                        _productService.Update(product.Id, product);
                        product.Quantity -= receivedMessage.UpdatedSale.NewQuantity;
                        _productService.Update(product.Id, product);
                    }
                    break;
                }
                case "SaleCancelled":
                {
                    product.Quantity += receivedMessage.UpdatedSale.OldQuantity;
                    _productService.Update(product.Id, product);
                    break;
                }
            }

            return Task.CompletedTask;
        }
    }
}
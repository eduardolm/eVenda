using System;
using System.Threading;
using System.Threading.Tasks;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;
using Microsoft.Azure.ServiceBus;

namespace eVendas.Sales.Helper
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
        public Task SendMessageAsync(MessageType messageType, Sale sale, UpdatedSale updatedSale=null)
        {
            var messageToSend = _factory.Create(messageType, sale, updatedSale);

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
            var receivedMessage = message.Body.ParseJson<WarehouseInputMessage>();
            var product = new Product(receivedMessage.ProductId, receivedMessage.Sku, receivedMessage.Name, 
                receivedMessage.Price, receivedMessage.Quantity, receivedMessage.CreatedAt, receivedMessage.UpdatedAt);

            switch (receivedMessage.MessageTitle)
            {
                case "ProductCreated":
                    _productService.Create(product);
                    break;
                case "ProductUpdated":
                    _productService.Update(receivedMessage.ProductId, product);
                    break;
                case "ProductDeleted":
                    _productService.Delete(receivedMessage.ProductId);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
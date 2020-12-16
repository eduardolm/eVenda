using System;
using System.Threading;
using System.Threading.Tasks;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eVendas.Sales.Helper
{
    public class BusListener : IHostedService
    {
        private readonly ILogger _logger;
        private SubscriptionClient _subscriptionClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IProductService _productService;

        public BusListener(ILoggerFactory loggerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = loggerFactory.CreateLogger<BusListener>();
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _productService = scope.ServiceProvider.GetService<IProductService>();
                
                _logger.LogDebug($"BusListener starting; Registering message handler.");
                _subscriptionClient = new SubscriptionClient("Endpoint=sb://evenda-service-bus.servicebus.windows.net/;SharedAccessKeyName=ListenOnly;SharedAccessKey=d4ggwX0BK5c9zrS1yEWduJF/ac560r4SFw5so9551k8=", "vendarealizada", "VendaRealizadaEstoque");
            
                var messageHandlerOptions = new MessageHandlerOptions(e => {
                    ProcessError(e.Exception);
                    return Task.CompletedTask;
                })
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };
            
                _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

                return Task.CompletedTask;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"BusListenerService stopping.");
            await _subscriptionClient.CloseAsync();
        }

        private Task ProcessMessageAsync(Message message, CancellationToken arg2)
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

        private void ProcessError(Exception e) {
            _logger.LogError(e, "Error while processing messages item in BusListenerService.");
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eVendas.Warehouse.Helper
{
    public class BusListener : IHostedService
    {
        private readonly ILogger _logger;
        private SubscriptionClient _subscriptionClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IProductService _productService;
        private IUpdateProduct _updateProduct;

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
                _updateProduct = scope.ServiceProvider.GetService<IUpdateProduct>();
                
                _logger.LogDebug($"BusListener starting; Registering message handler.");
                _subscriptionClient = new SubscriptionClient("Endpoint=sb://evenda-service-bus.servicebus.windows.net/;SharedAccessKeyName=StockReceiveOnly;SharedAccessKey=WxUBzyLeZCmEMih9p58QgH4FSXjVeOgRV1VVjuGvGZk=", "sale-send", "Sale-Message");
            
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
            _logger.LogDebug($"BusListener stopping.");
            await _subscriptionClient.CloseAsync();
        }

        public Task ProcessMessageAsync(Message message, CancellationToken arg2)
        {
            Console.WriteLine(message.Body.ToString());
            var receivedMessage = message.Body.ParseJson<SaleInputMessage>();
            Console.WriteLine(receivedMessage);
            var sale = new Sale(receivedMessage.SaleId, receivedMessage.ProductId, receivedMessage.Quantity, 
                receivedMessage.CreatedAt, receivedMessage.UpdatedAt);
            
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _productService = scope.ServiceProvider.GetService<IProductService>();
                _updateProduct = scope.ServiceProvider.GetService<IUpdateProduct>();
                var product = _productService.GetById(receivedMessage.ProductId);
                _updateProduct.UpdateItem(product, sale, receivedMessage);

                return Task.CompletedTask;
            }
        }

        private void ProcessError(Exception e) {
            _logger.LogError(e, "Error while processing messages item in BusListener.");
        }
    }
}
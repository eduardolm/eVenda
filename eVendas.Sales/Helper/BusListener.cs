using System;
using System.Threading;
using System.Threading.Tasks;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
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
        private string _busListenerConnectionString;
        private IConfiguration Configuration { get; }

        public BusListener(ILoggerFactory loggerFactory, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = loggerFactory.CreateLogger<BusListener>();
            Configuration = configuration;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _busListenerConnectionString = Configuration["ListenerConnectionString"];
            
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _productService = scope.ServiceProvider.GetService<IProductService>();
                
                _logger.LogDebug($"BusListener starting; Registering message handler.");
                _subscriptionClient = new SubscriptionClient(_busListenerConnectionString, "stock-send", "Stock_Message");
            
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

        private Task ProcessMessageAsync(Message message, CancellationToken arg2)
        {
            var receivedMessage = message.Body.ParseJson<WarehouseInputMessage>();
            var product = new Product(receivedMessage.Sku, receivedMessage.Name, 
                receivedMessage.Price, receivedMessage.Quantity, receivedMessage.CreatedAt, receivedMessage.UpdatedAt);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _productService = scope.ServiceProvider.GetService<IProductService>();
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

        private void ProcessError(Exception e) {
            _logger.LogError(e, "Error while processing messages item in BusListener.");
        }
    }
}
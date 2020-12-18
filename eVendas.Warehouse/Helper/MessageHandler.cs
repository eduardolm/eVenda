using System;
using System.Threading.Tasks;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace eVendas.Warehouse.Helper
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IMessageFactory _factory;
        private IConfiguration Configuration { get; }
        private string _connectionString;
        public MessageHandler(IMessageFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            Configuration = configuration;
        }
        public Task SendMessageAsync(MessageType messageType, Product product)
        {
            _connectionString = Configuration["MessageConnectionString"];
            var messageToSend = _factory.Create(messageType, product);

            var serviceBusClient = new TopicClient(_connectionString, "stock-send");

            var message = new Message(messageToSend.ToJsonBytes());
            message.ContentType = "application/json";
            message.UserProperties.Add("CorrelationId", Guid.NewGuid().ToString());

            serviceBusClient.SendAsync(message);

            return Task.CompletedTask;
        }
    }
}
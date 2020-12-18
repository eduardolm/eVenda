using System;
using System.Threading.Tasks;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace eVendas.Sales.Helper
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IMessageFactory _factory;
        private string _messageConnectionString;
        private IConfiguration Configuration { get; }
        public MessageHandler(IMessageFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            Configuration = configuration;
        }
        public Task SendMessageAsync(MessageType messageType, Sale sale, UpdatedSale updatedSale=null)
        {

            _messageConnectionString = Configuration["MessageConnectionString"];
            var messageToSend = _factory.Create(messageType, sale, updatedSale);

            var serviceBusClient = new TopicClient(_messageConnectionString, "sale-send");

            var message = new Message(messageToSend.ToJsonBytes());
            message.ContentType = "application/json";
            message.UserProperties.Add("CorrelationId", Guid.NewGuid().ToString());

            serviceBusClient.SendAsync(message);

            return Task.CompletedTask;
        }
    }
}
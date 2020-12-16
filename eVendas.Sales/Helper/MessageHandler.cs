using System;
using System.Threading.Tasks;
using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using Microsoft.Azure.ServiceBus;

namespace eVendas.Sales.Helper
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IMessageFactory _factory;

        public MessageHandler(IMessageFactory factory)
        {
            _factory = factory;
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
    }
}
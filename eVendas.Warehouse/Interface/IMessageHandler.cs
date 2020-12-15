using System.Threading;
using System.Threading.Tasks;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Model;
using Microsoft.Azure.ServiceBus;

namespace eVendas.Warehouse.Interface
{
    public interface IMessageHandler
    {
        Task SendMessageAsync(MessageType messageType, Product product);
        Task ReceiveMessageAsync();
        Task ProcessMessageAsync(Message message, CancellationToken arg2);
    }
}
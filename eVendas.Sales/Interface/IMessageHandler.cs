using System.Threading;
using System.Threading.Tasks;
using eVendas.Sales.Enum;
using eVendas.Sales.Model;
using Microsoft.Azure.ServiceBus;

namespace eVendas.Sales.Interface
{
    public interface IMessageHandler
    {
        Task SendMessageAsync(MessageType messageType, Sale sale, UpdatedSale updatedSale=null);
    }
}
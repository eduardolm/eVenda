using System.Threading.Tasks;
using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Model;

namespace eVendas.Warehouse.Interface
{
    public interface IMessageHandler
    {
        Task SendMessageAsync(MessageType messageType, Product product);
    }
}
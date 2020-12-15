using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;

namespace eVendas.Warehouse.Service.MessageHandlerFactory
{
    public class MessageFactory : IMessageFactory
    {
        public WarehouseOutputMessage Create(MessageType messageType, Product product)
        {
            return new WarehouseOutputMessage(messageType, product);
        }
    }
}
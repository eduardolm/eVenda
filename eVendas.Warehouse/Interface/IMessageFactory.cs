using eVendas.Warehouse.Enum;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;

namespace eVendas.Warehouse.Interface
{
    public interface IMessageFactory
    {
        WarehouseOutputMessage Create(MessageType messageType, Product product);
    }
}
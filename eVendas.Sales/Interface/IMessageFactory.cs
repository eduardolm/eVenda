using eVendas.Sales.Enum;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;

namespace eVendas.Sales.Interface
{
    public interface IMessageFactory
    {
        SaleOutputMessage Create(MessageType messageType, Sale sale, UpdatedSale updatedSale=null);
    }
}
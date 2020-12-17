using eVendas.Sales.Enum;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Model.MessageFactoryModel;

namespace eVendas.Sales.Service.MessageHandlerFactory
{
    public class MessageFactory : IMessageFactory
    {
        public SaleOutputMessage Create(MessageType messageType, Sale sale, UpdatedSale updatedSale=null)
        {
            return new SaleOutputMessage(messageType, sale, updatedSale);
        }
    }
}
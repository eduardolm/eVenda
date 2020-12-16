using eVendas.Warehouse.Model;
using eVendas.Warehouse.Model.MessageFactoryModel;

namespace eVendas.Warehouse.Interface
{
    public interface IUpdateProduct
    {
        void UpdateItem(Product product, Sale sale, SaleInputMessage receivedMessage);
    }
}
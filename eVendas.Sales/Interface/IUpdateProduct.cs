using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface IUpdateProduct
    {
        void UpdateStock(Sale newSale, Sale oldSale=null);
        void CancelSale(Sale sale);
    }
}
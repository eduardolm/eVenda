using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface ISaleService : IGenericService<Sale>
    {
        new object Create(Sale sale);
        new object Update(int id, Sale sale);
    }
}
using System.Threading.Tasks;
using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface ISaleService : IGenericService<Sale>
    {
        new Task<object> Create(Sale sale);
        new Task<object> Update(int id, Sale sale);
        new Task<object> Delete(int id);
    }
}
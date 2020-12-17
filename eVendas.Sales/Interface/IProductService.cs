using System.Collections.Generic;
using System.Threading.Tasks;
using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface IProductService : IGenericService<Product>
    {
        new IEnumerable<Product> GetAll();
        new Product GetById(int id);
        new object Create(Product product);
        new object Update(int id, Product product);
    }
}
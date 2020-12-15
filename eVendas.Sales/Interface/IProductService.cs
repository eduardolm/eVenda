using eVendas.Sales.Model;

namespace eVendas.Sales.Interface
{
    public interface IProductService : IGenericService<Product>
    {
        new object Create(Product product);
        new object Update(int id, Product product);
    }
}
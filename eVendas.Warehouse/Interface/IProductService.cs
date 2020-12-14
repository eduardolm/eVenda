using eVendas.Warehouse.Model;

namespace eVendas.Warehouse.Interface
{
    public interface IProductService : IGenericService<Product>
    {
        new object Create(Product product);
        new object Update(int id, Product product);
    }
}
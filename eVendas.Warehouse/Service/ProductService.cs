using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using eVendas.Warehouse.Service.GenericService;
using FluentValidation;

namespace eVendas.Warehouse.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(IGenericRepository<Product> repository, 
            IValidator<Product> validator) : base(repository, validator)
        {
        }
    }
}
using eVendas.Sales.Context;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using eVendas.Sales.Repository.GenericRepository;

namespace eVendas.Sales.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(MainContext context) : base(context)
        {
        }
    }
}
using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    public class UpdatedSale : Base, IBase
    {
        public int OldProductId { get; set; }
        public int NewProductId { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }

        public UpdatedSale(int oldProductId, int newProductId, int oldQuantity, int newQuantity)
        {
            OldProductId = oldProductId;
            NewProductId = newProductId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }
}
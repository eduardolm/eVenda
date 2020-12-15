using eVendas.Sales.Interface;

namespace eVendas.Sales.Model
{
    public class UpdatedSale : Base, IBase
    {
        private int OldProductId { get; set; }
        private int NewProductId { get; set; }
        private int OldQuantity { get; set; }
        private int NewQuantity { get; set; }

        public UpdatedSale(int oldProductId, int newProductId, int oldQuantity, int newQuantity)
        {
            OldProductId = oldProductId;
            NewProductId = newProductId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }
}
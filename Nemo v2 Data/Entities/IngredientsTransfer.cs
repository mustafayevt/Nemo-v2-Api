using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class IngredientsTransfer:BaseEntity
    {
        [Required,ForeignKey(nameof(Ingredient))]
        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        
        [Required,ForeignKey(nameof(WarehouseTransferInvoice))]
        public long WarehouseTransferInvoiceId { get; set; }
        public virtual WarehouseTransferInvoice WarehouseTransferInvoice { get; set; }
        
        public decimal Quantity { get; set; }
        public decimal PriceForEach { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
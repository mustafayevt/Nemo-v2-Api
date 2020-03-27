using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class IngredientsInsert:BaseEntity
    {
        [Required,ForeignKey(nameof(Ingredient))]
        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        
        [Required,ForeignKey(nameof(WarehouseInvoice))]
        public long WarehouseInvoiceId { get; set; }
        public virtual WarehouseInvoice WarehouseInvoice { get; set; }
        
        public decimal InitialCount { get; set; }
        public decimal CurrentCount { get; set; }
        public decimal PriceForEach { get; set; }
    }
}
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
        
        public decimal Quantity { get; set; }
        
        [Required]
        public Unit Unit { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required] 
        public decimal MinimumQuantity { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
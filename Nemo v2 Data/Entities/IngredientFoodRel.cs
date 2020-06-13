using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class IngredientFoodRel
    {
        [Required,ForeignKey(nameof(Food))]
        public long FoodId { get; set; }
        [Required,ForeignKey(nameof(Ingredient))]
        public long IngredientId { get; set; }
        [Required,ForeignKey(nameof(Warehouse))]
        public long WarehouseId { get; set; }

        public virtual Food Food { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public Unit Unit { get; set; }
    }
}
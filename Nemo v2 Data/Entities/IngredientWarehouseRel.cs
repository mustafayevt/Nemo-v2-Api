using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class IngredientWarehouseRel
    {
        [Required, ForeignKey(nameof(Warehouse))]
        public long WarehouseId { get; set; }

        [Required, ForeignKey(nameof(Ingredient))]
        public long IngredientId { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        [DefaultValue(0)]
        public decimal? Quantity { get; set; }
    }
}
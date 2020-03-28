using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Ingredient:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required,ForeignKey(nameof(Warehouse))]
        public long WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        
        public  List<IngredientCategoryRel> IngredientCategoryRels { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Ingredient:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        
        public  List<IngredientCategoryRel> IngredientCategories { get; set; }

        public List<IngredientWarehouseRel> IngredientWarehouseRels { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
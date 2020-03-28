using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Food:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required,ForeignKey(nameof(FoodGroup))]
        public long FoodGroupId { get; set; }
        
        public virtual FoodGroup FoodGroup { get; set; }

        public List<IngredientFoodRel> Ingredients { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
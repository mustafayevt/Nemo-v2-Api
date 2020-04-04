using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Food:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }

        public  List<FoodGroupRel> FoodGroups { get; set; }

        public List<IngredientFoodRel> Ingredients { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
        

        [Required,ForeignKey(nameof(Printer))]
        public long PrinterId { get; set; }
        
        public virtual Printer Printer { get; set; }

    }
}
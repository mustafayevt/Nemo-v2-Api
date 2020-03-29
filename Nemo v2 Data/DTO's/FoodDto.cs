using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class FoodDto
    {
        public long Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }

        public  List<FoodGroupDto> FoodGroups { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class IngredientDto
    {
        [Required] 
        public long Id { get; set; }
        
        [Required,MaxLength(50)]
        public string Name { get; set; }

        public List<IngredientCategoryDto> IngredientCategories { get; set; }

        public List<WarehouseDto> IngredientWarehouseRels { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
        public decimal CurrentQuantity { get; set; }
        public Unit Unit { get; set; }

        public decimal AvgPriceByWarehouse { get; set; }

    }
}
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class IngredientDto
    {
        [Required] 
        public long Id { get; set; }
        
        [Required,MaxLength(50)]
        public string Name { get; set; }

        public long WarehouseId { get; set; }

        public List<IngredientCategoryDto> IngredientCategories { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
    }
}
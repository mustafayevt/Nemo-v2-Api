using System;
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

        public List<KeyValuePair<long,decimal>> Ingredients { get; set; }

        public List<KeyValuePair<SectionDto,PrinterDto>> SectionToPrinter { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }

        [Required]
        public decimal Cost { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
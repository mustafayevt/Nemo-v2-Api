using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class WarehouseDto
    {
        [Required]
        public long Id { get; set; }
        [Required,MaxLength(Int32.MaxValue)]
        public string Name { get; set; }

        public List<RestaurantDto> RestWareRels { get; set; }
    }
}
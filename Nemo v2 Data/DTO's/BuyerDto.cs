using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class BuyerDto
    {
        public long Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }

        // [Required]
        // public long RestaurantId { get; set; }

        public IEnumerable<RestaurantDto> RestBuyerRels { get; set; }
    }
}
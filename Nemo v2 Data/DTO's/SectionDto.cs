using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class SectionDto
    {
        public long Id { get; set; }
        [Required,MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public long RestaurantId { get; set; }
        
        public List<TableDto> Tables { get; set; }
    }
}
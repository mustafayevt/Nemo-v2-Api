using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class TableDto
    {
        public long Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public long SectionId { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
        [Required]
        public int ChairCount { get; set; }
    }
}
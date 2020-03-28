using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Table:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }

        [Required,ForeignKey(nameof(Section))]
        public long SectionId { get; set; }
        
        public virtual Section Section { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
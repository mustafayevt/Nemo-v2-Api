using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class FoodGroupRel
    {
        [Required,ForeignKey(nameof(Food))]
        public long FoodId { get; set; }
        [Required,ForeignKey(nameof(FoodGroup))]
        public long FoodGroupId { get; set; }

        public virtual Food Food { get; set; }
        public virtual FoodGroup FoodGroup { get; set; }
    }
}
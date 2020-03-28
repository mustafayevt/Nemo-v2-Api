using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class IngredientCategoryRel
    {
        [Required,ForeignKey(nameof(Ingredient))]
        public long IngredientId { get; set; }
        [Required,ForeignKey(nameof(IngredientCategory))]
        public long IngredientCategoryId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual IngredientCategory IngredientCategory { get; set; }
    }
}
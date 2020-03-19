using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data.Entities
{
    public class IngredientCategory:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
    }
}
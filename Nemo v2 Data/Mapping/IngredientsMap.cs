using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class IngredientFoodRelMap
    {
        public IngredientFoodRelMap(EntityTypeBuilder<IngredientFoodRel> entityBuilder)
        {
            entityBuilder.HasKey(x => new {x.FoodId, x.IngredientId});
        } 
    }
}
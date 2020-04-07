using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Maping
{
    public class IngredientWarehouseRelMap
    {
        public IngredientWarehouseRelMap(EntityTypeBuilder<IngredientWarehouseRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.WarehouseId, t.IngredientId});
        }
    }
}
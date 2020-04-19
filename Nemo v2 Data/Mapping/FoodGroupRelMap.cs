using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class FoodGroupRelMap
    {
        public FoodGroupRelMap(EntityTypeBuilder<FoodGroupRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.FoodId, t.FoodGroupId});
        } 
    }
}
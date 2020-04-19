using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class RestWareRelMap
    {
        public RestWareRelMap(EntityTypeBuilder<RestWareRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.RestaurantId, t.WarehouseId});
        } 
    }
}
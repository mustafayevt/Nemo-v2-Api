using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Maping
{
    public class RestSupplierRelMap
    {
        public RestSupplierRelMap(EntityTypeBuilder<RestSupplierRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.RestaurantId, t.SupplierId});
        } 
    }
}
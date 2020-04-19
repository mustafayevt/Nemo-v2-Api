using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class ProfitMap
    {
        public ProfitMap(EntityTypeBuilder<Profit> entityBuilder)
        {
            entityBuilder.HasIndex(x => new {x.RestaurantId}).IsUnique();
        }
    }
}
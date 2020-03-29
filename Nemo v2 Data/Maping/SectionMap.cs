using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Maping
{
    public class SectionMap
    {
        public SectionMap(EntityTypeBuilder<Section> entityBuilder)  
        {  
            entityBuilder
                .HasMany<Table>(g => g.Tables)
                .WithOne(s => s.Section)
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}
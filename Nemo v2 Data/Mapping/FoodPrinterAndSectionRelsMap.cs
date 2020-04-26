using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class FoodPrinterAndSectionRelsMap
    {
        public FoodPrinterAndSectionRelsMap(EntityTypeBuilder<FoodPrinterAndSectionRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.FoodId, t.PrinterId,t.SectionId});
        } 
    }
}
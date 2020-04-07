using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Maping
{
    public class FoodInvoiceRelMap
    {
        public FoodInvoiceRelMap(EntityTypeBuilder<FoodInvoiceRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.FoodId, t.InvoiceId});
        }
    }
}
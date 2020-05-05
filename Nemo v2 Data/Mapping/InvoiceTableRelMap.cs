using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class InvoiceTableRelMap
    {
        public InvoiceTableRelMap(EntityTypeBuilder<InvoiceTableRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.TableId, t.InvoiceId});
        }
    }
}
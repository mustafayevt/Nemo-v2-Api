using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class PaymentTypeInvoiceRelMap
    {
        public PaymentTypeInvoiceRelMap(EntityTypeBuilder<PaymentTypeInvoiceRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.PaymentTypeId, t.InvoiceId});
        } 
    }
}
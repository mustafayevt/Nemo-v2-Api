using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class PaymentTypeInvoiceRel
    {
        [Required,ForeignKey(nameof(Invoice))]
        public long InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        
        [Required,ForeignKey(nameof(PaymentType))]
        public long PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }

        public decimal Amount { get; set; }
    }
}
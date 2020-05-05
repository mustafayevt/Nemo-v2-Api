using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class InvoiceTableRel
    {
        [Required, ForeignKey(nameof(Table))] 
        public long TableId { get; set; }

        [Required, ForeignKey(nameof(Invoice))]
        public long InvoiceId { get; set; }

        public virtual Table Table { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
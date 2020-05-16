using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class FoodInvoiceRel
    {
        [Required, ForeignKey(nameof(Food))] 
        public long FoodId { get; set; }

        [Required, ForeignKey(nameof(Invoice))]
        public long InvoiceId { get; set; }

        public virtual Food Food { get; set; }
        public virtual Invoice Invoice { get; set; }

        public int Count { get; set; }
    }
}
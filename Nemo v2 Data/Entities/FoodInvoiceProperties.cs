using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class FoodInvoiceProperties
    {
        public long Id { get; set; }

        // [Required, ForeignKey(nameof(FoodInvoiceRel))]
        // public long FoodInvoiceRelId { get; set; }
        //
        // public virtual FoodInvoiceRel FoodInvoiceRel { get; set; }

        public FoodSaleType FoodSaleType { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal ChangedPrice { get; set; }
    }
}
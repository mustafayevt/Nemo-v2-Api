using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class FoodInvoiceProperties
    {
        public long Id { get; set; }

        public FoodSaleType FoodSaleType { get; set; }
        public int Count { get; set; }
        public float Portion { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal ChangedPrice { get; set; }
        [ForeignKey(nameof(Table))]
        public long TableId { get; set; }

        public Table Table { get; set; }
        
        [Required,ForeignKey(nameof(User))]
        public long UserId { get; set; }

        public User User { get; set; }
    }
}
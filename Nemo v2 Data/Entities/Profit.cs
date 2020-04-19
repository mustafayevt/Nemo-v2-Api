using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Profit:BaseEntity
    {
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public decimal VAT { get; set; }

        public decimal IngredientProfit { get; set; }

        public decimal ProductProfit { get; set; }
    }
}
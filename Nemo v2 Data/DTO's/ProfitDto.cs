using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class ProfitDto
    {
        public long Id { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
        
        public decimal VAT { get; set; }

        public decimal IngredientProfit { get; set; }

        public decimal ProductProfit { get; set; }
    }
}
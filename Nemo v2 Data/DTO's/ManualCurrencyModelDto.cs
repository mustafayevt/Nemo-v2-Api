using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class ManualCurrencyModelDto
    {
        public long Id { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        [Required]
        public bool MainCurrency { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
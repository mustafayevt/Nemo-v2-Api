using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class PaymentTypeDto
    {
        public long Id { get; set; }
        [Required]
        [MinLength(3)]
        public string TypeName { get; set; }
        [Required]
        public long RestaurantId { get; set; }
    }
}
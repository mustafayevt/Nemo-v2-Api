using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class RestBuyerRel
    {
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        [Required,ForeignKey(nameof(Buyer))]
        public long BuyerId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Buyer Buyer { get; set; }
    }
}
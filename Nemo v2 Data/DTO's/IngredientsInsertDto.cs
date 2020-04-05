using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class IngredientsInsertDto
    {
        public long Id { get; set; }
        [Required]
        public long IngredientId { get; set; }
        
        [Required]
        public long WarehouseInvoiceId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal PriceForEach { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
    }
}
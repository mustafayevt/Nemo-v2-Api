using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class WarehouseExportInvoiceDto
    {
        public long Id { get; set; }
        [Required,MaxLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public long BuyerId { get; set; }
        
        [Required]
        public long UserId { get; set; }
        
        [Required]
        public long WarehouseId { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }

        public bool IsPayed { get; set; }
        public List<IngredientsExportDto> IngredientsExports { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
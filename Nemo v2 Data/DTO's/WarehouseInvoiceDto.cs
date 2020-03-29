using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class WarehouseInvoiceDto
    {
        public long Id { get; set; }
        [Required,MaxLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public long SupplierId { get; set; }
        
        [Required]
        public long UserId { get; set; }
        
        [Required]
        public long WarehouseId { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }

        public bool IsPayed { get; set; }
        public List<IngredientsInsertDto> IngredientsInserts { get; set; }
    }
}
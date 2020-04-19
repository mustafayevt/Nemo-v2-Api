using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nemo_v2_Data.Currency;

namespace Nemo_v2_Data
{
    public class WarehouseInvoiceDto
    {
        public long Id { get; set; }
        
        public DateTime Date { get; set; }
        
        [Required] 
        public DateTime PromisedDateTime { get; set; }
        
        [Required]
        public string InvoiceNumber { get; set; }

        public long ComputedNumber { get; set; }

        [Required]
        public long SupplierId { get; set; }
        
        [Required]
        public long UserId { get; set; }
        
        [Required]
        public long WarehouseId { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
        
        [Required] 
        public string SupplierAdress { get; set; }
        
        [Required]
        public string ResponsiblePerson { get; set; }

        [Required] 
        public decimal TotalAmount { get; set; }

        [Required] 
        public decimal Discount { get; set; }
        
        [Required] 
        public decimal VAT { get; set; }
        
        [Required]
        public string ValuteValue { get; set; }
        [Required]
        public string ValuteCode { get; set; }
        public string Note { get; set; }

        
        public bool IsPayed { get; set; }
        public List<IngredientsInsertDto> IngredientsInserts { get; set; }
    }
}
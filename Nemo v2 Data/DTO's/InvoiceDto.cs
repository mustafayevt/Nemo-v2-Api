using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class InvoiceDto
    {
        public long Id { get; set; }
        [Required]
        public InvoiceType InvoiceType { get; set; }
        
        [Required]
        public long RestaurantId { get; set; }
        
        [Required]
        public long SectionId { get; set; }
        
        [Required]
        public long OpenedUserId { get; set; }
        
        [Required]
        public long ClosedUserId { get; set; }
        


        public List<TableDto> InvoiceTableRels { get; set; }

        
        public List<FoodDto> Foods { get; set; }
        
        public short PeopleCount { get; set; }
        
        public bool IsIngredientReduced { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
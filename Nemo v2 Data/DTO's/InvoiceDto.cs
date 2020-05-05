﻿using System.Collections.Generic;
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
        public long UserId { get; set; }


        public List<TableDto> InvoiceTableRels { get; set; }

        
        public List<FoodDto> Foods { get; set; }
        public bool IsIngredientReducted { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
    }
}
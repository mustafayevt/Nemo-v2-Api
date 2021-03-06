﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class WarehouseInvoice : BaseEntity
    {
        [Required]
        public string InvoiceNumber { get; set; }

        public long ComputedNumber { get; set; }

        [Required, ForeignKey(nameof(Supplier))]
        public long SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        [Required, ForeignKey(nameof(User))] public long UserId { get; set; }
        public virtual User User { get; set; }

        [Required, ForeignKey(nameof(Warehouse))]
        public long WarehouseId { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        [Required, ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
        
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
        
        [Required]
        public DateTime Date { get; set; }
        [Required] 
        public DateTime PromisedDateTime { get; set; }

        public string Note { get; set; }

        public bool IsPayed { get; set; }
        public List<IngredientsInsert> IngredientsInserts { get; set; }
    }
}
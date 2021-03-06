﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Role:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        
        //permissions
        public bool CanFinishDay { get; set; }
        public bool CanAddProduct { get; set; }
        public bool CanVoidProduct { get; set; }
        public bool CanTransferInvoice { get; set; }
        public bool CanMergeInvoices { get; set; }
        public bool CanCloseCheck { get; set; }
        public bool CanInsertIngredient { get; set; }
        public bool CanShowStock { get; set; }
        public bool CanShowProductTransfers { get; set; }
        public bool CanExit { get; set; }
    }
}
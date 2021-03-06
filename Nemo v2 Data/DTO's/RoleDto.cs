﻿using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class RoleDto
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        
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
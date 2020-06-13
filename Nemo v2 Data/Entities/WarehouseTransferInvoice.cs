using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class WarehouseTransferInvoice:BaseEntity
    {
        [Required]
        public string InvoiceNumber { get; set; }
        public ICollection<IngredientsTransfer> Ingredients { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime AcceptedTime { get; set; }
        public bool IsPayed { get; set; }


        [Required,ForeignKey(nameof(User))]
        public long RequesterUserId { get; set; }
        public virtual User RequesterUser { get; set; }
        
        [Required,ForeignKey(nameof(User))]
        public long AcceptorUserId { get; set; }
        public virtual User AcceptorUser { get; set; }
        
        
        
        [Required,ForeignKey(nameof(Warehouse))]
        public long RequesterWarehouseId { get; set; }
        public virtual Warehouse RequesterWarehouse { get; set; }
        
        [Required,ForeignKey(nameof(AcceptorWarehouse))]
        public long AcceptorWarehouseId { get; set; }
        public virtual Warehouse AcceptorWarehouse { get; set; }
        
        
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}
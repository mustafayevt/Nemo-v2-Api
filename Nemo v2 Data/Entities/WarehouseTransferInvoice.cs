using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class WarehouseTransferInvoice:BaseEntity
    {
        [Required,MaxLength(50)]
        public string InvoiceNumber { get; set; }

        [Required,ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        
        
        
        [Required,ForeignKey(nameof(Warehouse))]
        public long WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        
        [Required,ForeignKey(nameof(AcceptorWarehouse))]
        public long AcceptorWarehouseId { get; set; }
        public virtual Warehouse AcceptorWarehouse { get; set; }
        
        
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public bool IsPayed { get; set; }
        public List<IngredientsTransfer> IngredientsTransfers { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
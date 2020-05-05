using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Invoice:BaseEntity
    {
        [Required]
        public InvoiceType InvoiceType { get; set; }
        
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        
        
        [Required,ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }


        public virtual List<InvoiceTableRel> InvoiceTableRels { get; set; }

        
        public List<FoodInvoiceRel> Foods { get; set; }
        [DefaultValue(false)]
        public bool IsIngredientReducted { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
    }
}
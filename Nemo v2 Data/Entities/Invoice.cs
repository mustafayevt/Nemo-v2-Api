using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Invoice:BaseEntity
    {
        public string InvoiceNumber { get; set; }
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        
        
        [Required,ForeignKey(nameof(User))]
        public long OpenedUserId { get; set; }
        public virtual User OpenedUser { get; set; }
        
        [Required,ForeignKey(nameof(User))]
        public long ClosedUserId { get; set; }
        public virtual User ClosedUser { get; set; }


        [Required,ForeignKey(nameof(Section))]
        public long SectionId { get; set; }
        public virtual Section Section { get; set; }
        
        
        public virtual IEnumerable<InvoiceTableRel> InvoiceTableRels { get; set; }
        
        public List<FoodInvoiceRel> Foods { get; set; }
        
        public short PeopleCount { get; set; }
        
        [DefaultValue(false)]
        public bool IsIngredientReduced { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<PaymentTypeInvoiceRel> PaymentTypeInvoiceRels { get; set; }
    }
}
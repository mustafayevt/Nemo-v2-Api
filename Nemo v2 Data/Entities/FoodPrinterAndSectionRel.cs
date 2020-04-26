using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Nemo_v2_Data.Entities
{
    public class FoodPrinterAndSectionRel
    {
        [Required, ForeignKey(nameof(Food))] 
        public long FoodId { get; set; }

        [Required, ForeignKey(nameof(Printer))]
        public long PrinterId { get; set; }
        
        [Required, ForeignKey(nameof(Section))]
        public long SectionId { get; set; }

        public virtual Food Food { get; set; }
        public virtual Printer Printer { get; set; }
        public virtual Section Section { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data.Entities
{
    public class Buyer:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        
        public IEnumerable<RestBuyerRel> RestBuyerRels { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Supplier:BaseEntity
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        
        public IEnumerable<RestSupplierRel> RestSupplierRels { get; set; }

    }
}
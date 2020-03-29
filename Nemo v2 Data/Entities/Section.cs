using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Nemo_v2_Data.Entities
{
    public class Section:BaseEntity
    {
        [Required,MaxLength(30)]
        public string Name { get; set; }

        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public List<Table> Tables { get; set; }
    }
}
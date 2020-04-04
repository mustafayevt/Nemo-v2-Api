using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class RestaurantDto
    {
        public long Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public long? BranchId { get; set; }
        // public List<RestaurantDto> Branches { get; set; }
        public List<SectionDto> Sections { get; set; }
    }
}
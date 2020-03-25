using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data
{
    public class RestaurantDto
    {
        public long Id { get; set; }
        [Required] public string Name { get; set; }
        public long? BranchId { get; set; }
    }
}
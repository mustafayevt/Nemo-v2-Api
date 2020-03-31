using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class RestaurantDto
    {
        public long Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public long? BranchId { get; set; }
    }
}
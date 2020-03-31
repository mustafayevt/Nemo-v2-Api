using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data
{
    public class UserDto
    {
        [Required]
        // [Range(1,long.MaxValue)]
        public long Id { get; set; }
        [Required] 
        public string Firstname { get; set; }
        [Required] 
        public string Lastname { get; set; }
        [Required] 
        public string Password { get; set; }
        [Required] 
        public long RestaurantId { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}
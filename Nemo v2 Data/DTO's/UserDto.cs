using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data
{
    public class UserDto
    {
        [Required]
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
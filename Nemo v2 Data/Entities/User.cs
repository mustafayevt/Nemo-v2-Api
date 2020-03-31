using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class User:BaseEntity
    {
        [Required,MaxLength(30)]
        public string Firstname { get; set; }
        [Required,MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
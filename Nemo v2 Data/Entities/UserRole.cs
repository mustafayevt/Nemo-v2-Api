using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class UserRole:BaseEntity
    {
        [Required,ForeignKey(nameof(User))]
        public long UserId { get; set; }
        [Required,ForeignKey(nameof(Role))]
        public long RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
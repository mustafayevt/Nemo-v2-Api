using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Maping
{
    public class UserRoleMap
    {
        public UserRoleMap(EntityTypeBuilder<UserRole> entityBuilder)
        {
            entityBuilder.HasIndex(y => y.Id);
            entityBuilder.HasIndex(t=>new {t.UserId,t.RoleId}).IsUnique();

            // entityBuilder
            //     .HasOne<User>(l => l.User)
            //     .WithMany(a => a.UserRoles)
            //     .HasForeignKey(l => l.UserId);
            
            // entityBuilder
            //     .HasOne<Role>(l => l.Role)
            //     .WithMany(a => a)
            //     .HasForeignKey(l => l.UserId);

        } 
    }
}
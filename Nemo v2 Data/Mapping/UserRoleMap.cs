using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class UserRoleMap
    {
        public UserRoleMap(EntityTypeBuilder<UserRole> entityBuilder)
        {
            //entityBuilder.HasIndex(y => y.Id);
            entityBuilder.HasKey(t => new {t.UserId, t.RoleId});
            entityBuilder
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);

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
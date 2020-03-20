using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Maping
{
    public class UserMap  
    {  
        public UserMap(EntityTypeBuilder<User> entityBuilder)  
        {  
            entityBuilder.HasKey(t => t.Id);
        }  
    }  
}
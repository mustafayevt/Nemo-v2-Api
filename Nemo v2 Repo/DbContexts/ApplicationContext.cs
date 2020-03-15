using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Data.Maping;

namespace Nemo_v2_Repo.DbContexts
{
    public class ApplicationContext : DbContext  
    {  
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)  
        {  
        }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {  
            base.OnModelCreating(modelBuilder);  
            new UserMap(modelBuilder.Entity<User>());  
        }  
    }  
}
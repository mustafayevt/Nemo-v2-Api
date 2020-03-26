using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFRoleRepository:EFRepository<Role>
    {
        public EFRoleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
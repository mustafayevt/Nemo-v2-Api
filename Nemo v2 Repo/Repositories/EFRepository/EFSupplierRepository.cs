using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFSupplierRepository:EFRepository<Supplier>
    {
        public EFSupplierRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
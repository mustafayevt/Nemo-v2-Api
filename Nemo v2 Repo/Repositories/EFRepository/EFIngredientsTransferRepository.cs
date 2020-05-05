using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFIngredientsTransferRepository:EFRepository<IngredientsTransfer>,IIngredientsTransferRepository
    {
        public EFIngredientsTransferRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
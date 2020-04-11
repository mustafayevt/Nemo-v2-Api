using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFPrinterRepository:EFRepository<Printer>,IPrinterRepository
    {
        public EFPrinterRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Api.Hubs.Models;
using Nemo_v2_Data.SignalrModels.WarehouseTransfer;

namespace Nemo_v2_Repo.DbContexts
{
    public class HubTemporaryDataContext:DbContext
    {
        public HubTemporaryDataContext(DbContextOptions<HubTemporaryDataContext> options) : base(options)
        {
        }

        public DbSet<TransferIngredientModel> TransferIngredientModels { get; set; }
        public DbSet<InvoiceModel> InvoiceModels { get; set; }
    }
}
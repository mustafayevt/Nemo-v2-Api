using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Nemo_v2_Repo.Abstraction
{
    public interface IUnitOfWork
    {
        IBuyerRepository BuyerRepository { get; set; }
        IFoodGroupRepository FoodGroupRepository { get; set; }
        IFoodRepository FoodRepository { get; set; }
        IIngredientCategoryRepository IngredientCategoryRepository { get; set; }
        IIngredientRepository IngredientRepository { get; set; }
        IIngredientsExportRepository IngredientsExportRepository { get; set; }
        IIngredientsInsertRepository IngredientsInsertRepository { get; set; }
        IIngredientsTransferRepository IngredientsTransferRepository { get; set; }
        IInvoiceRepository InvoiceRepository { get; set; }
        IPrinterRepository PrinterRepository { get; set; }
        IRestaurantRepository RestaurantRepository { get; set; }
        IRoleRepository RoleRepository { get; set; }
        ISectionRepository SectionRepository { get; set; }
        ISupplierRepository SupplierRepository { get; set; }
        ITableRepository TableRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IWarehouseInvoiceRepository WarehouseInvoiceRepository { get; set; }
        IWarehouseExportInvoiceRepository WarehouseExportInvoiceRepository { get; set; }
        IWarehouseTransferInvoiceRepository WarehouseTransferInvoiceRepository { get; set; }
        IWarehouseRepository WarehouseRepository { get; set; }
        IProfitRepository ProfitRepository { get; set; }
        
        
        IDbContextTransaction CreateTransaction();
        void Commit();
        void Rollback();
        int Save();
    }
}
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Repositories.EFRepository;

namespace Nemo_v2_Repo.UnitOfWork
{
    public class EFUnitOfWork:IUnitOfWork
    {
        private readonly ApplicationContext _context;
        
        //Repositories
        public EFUnitOfWork(ApplicationContext context)
        {
            _context = context;
            BuyerRepository = new EFBuyerRepository(_context);
            FoodGroupRepository = new EFFoodGroupRepository(_context);
            FoodRepository = new EFFoodRepository(_context);
            IngredientCategoryRepository = new EFIngredientCategoryRepository(_context);
            IngredientRepository = new EFIngredientRepository(_context);
            IngredientsExportRepository = new EFIngredientsExportRepository(_context);
            IngredientsInsertRepository = new EFIngredientsInsertRepository(_context);
            InvoiceRepository = new EFInvoiceRepository(_context);
            PrinterRepository = new EFPrinterRepository(_context);
            RestaurantRepository = new EFRestaurantRepository(_context);
            RoleRepository = new EFRoleRepository(_context);
            SectionRepository = new EFSectionRepository(_context);
            SupplierRepository = new EFSupplierRepository(_context);
            TableRepository = new EFTableRepository(_context);
            UserRepository = new EFUserRepository(_context);
            WarehouseInvoiceRepository = new EFWarehouseInvoiceRepository(_context);
            WarehouseRepository = new EFWarehouseRepository(_context);
            WarehouseExportInvoiceRepository = new EFWarehouseExportInvoiceRepository(_context);
        }


        public IBuyerRepository BuyerRepository { get; set; }
        public IFoodGroupRepository FoodGroupRepository { get; set; }
        public IFoodRepository FoodRepository { get; set; }
        public IIngredientCategoryRepository IngredientCategoryRepository { get; set; }
        public IIngredientRepository IngredientRepository { get; set; }
        public IIngredientsExportRepository IngredientsExportRepository { get; set; }
        public IIngredientsInsertRepository IngredientsInsertRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }
        public IPrinterRepository PrinterRepository { get; set; }
        public IRestaurantRepository RestaurantRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public ISectionRepository SectionRepository { get; set; }
        public ISupplierRepository SupplierRepository { get; set; }
        public ITableRepository TableRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IWarehouseInvoiceRepository WarehouseInvoiceRepository { get; set; }
        public IWarehouseExportInvoiceRepository WarehouseExportInvoiceRepository { get; set; }
        public IWarehouseRepository WarehouseRepository { get; set; }

        public IDbContextTransaction CreateTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
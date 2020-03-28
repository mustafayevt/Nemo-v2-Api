using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Data.Maping;

namespace Nemo_v2_Repo.DbContexts
{
    public class ApplicationContext : DbContext  
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<RestWareRel> RestWareRels { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientsInsert> IngredientsInserts { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodGroup> FoodGroups { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WarehouseInvoice> WarehouseInvoices { get; set; }
        
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)  
        {  
        }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {  
            base.OnModelCreating(modelBuilder);  
            new UserMap(modelBuilder.Entity<User>());
            new UserRoleMap(modelBuilder.Entity<UserRole>());
            new RestWareRelMap(modelBuilder.Entity<RestWareRel>());
            new IngredientFoodRelMap(modelBuilder.Entity<IngredientFoodRel>());
            new IngredientCategoryRelMap(modelBuilder.Entity<IngredientCategoryRel>());
            new FoodGroupRelMap(modelBuilder.Entity<FoodGroupRel>());
        }
    }  
}
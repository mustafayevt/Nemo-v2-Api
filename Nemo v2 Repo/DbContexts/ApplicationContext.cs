using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Data.Maping;
using Npgsql;

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
        public DbSet<RestSupplierRel> RestSupplierRels { get; set; }
        public DbSet<IngredientCategory> IngredientCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientsInsert> IngredientsInserts { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodGroup> FoodGroups { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WarehouseInvoice> WarehouseInvoices { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Printer> Printers { get; set; }


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
            new FoodInvoiceRelMap(modelBuilder.Entity<FoodInvoiceRel>());
            new SectionMap(modelBuilder.Entity<Section>());
            new RestSupplierRelMap(modelBuilder.Entity<RestSupplierRel>());

            // modelBuilder.ForNpgsqlHasEnum<InvoiceType>();
            // NpgsqlConnection.GlobalTypeMapper.MapEnum<InvoiceType>();
            //
            // modelBuilder.ForNpgsqlHasEnum<Unit>();
            // NpgsqlConnection.GlobalTypeMapper.MapEnum<Unit>();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                e.State == EntityState.Added
                                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity) entityEntry.Entity).ModifiedDate = DateTime.Now;
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity) entityEntry.Entity).AddedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
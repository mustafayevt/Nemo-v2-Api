﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nemo_v2_Repo.DbContexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200329130840_TableSectionRelUpdate")]
    partial class TableSectionRelUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Nemo_v2_Data.Entities.Food", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("FoodGroupId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("FoodGroups");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodGroupRel", b =>
                {
                    b.Property<long>("FoodId");

                    b.Property<long>("FoodGroupId");

                    b.HasKey("FoodId", "FoodGroupId");

                    b.HasIndex("FoodGroupId");

                    b.ToTable("FoodGroupRel");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Ingredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RestaurantId");

                    b.Property<long>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("IngredientCategories");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientCategoryRel", b =>
                {
                    b.Property<long>("IngredientId");

                    b.Property<long>("IngredientCategoryId");

                    b.HasKey("IngredientId", "IngredientCategoryId");

                    b.HasIndex("IngredientCategoryId");

                    b.ToTable("IngredientCategoryRel");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientFoodRel", b =>
                {
                    b.Property<long>("FoodId");

                    b.Property<long>("IngredientId");

                    b.HasKey("FoodId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientFoodRel");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientsInsert", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("CurrentCount");

                    b.Property<long>("IngredientId");

                    b.Property<decimal>("InitialCount");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("PriceForEach");

                    b.Property<long>("RestaurantId");

                    b.Property<long>("WarehouseInvoiceId");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("WarehouseInvoiceId");

                    b.ToTable("IngredientsInserts");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestWareRel", b =>
                {
                    b.Property<long>("RestaurantId");

                    b.Property<long>("WarehouseId");

                    b.HasKey("RestaurantId", "WarehouseId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("RestWareRels");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Restaurant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long?>("BranchId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<bool>("CanAddProduct");

                    b.Property<bool>("CanCloseCheck");

                    b.Property<bool>("CanExit");

                    b.Property<bool>("CanFinishDay");

                    b.Property<bool>("CanInsertIngredient");

                    b.Property<bool>("CanMergeInvoices");

                    b.Property<bool>("CanShowProductTransfers");

                    b.Property<bool>("CanShowStock");

                    b.Property<bool>("CanTransferInvoice");

                    b.Property<bool>("CanVoidProduct");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Section", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Supplier", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Table", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<int>("ChairCount");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RestaurantId");

                    b.Property<long>("SectionId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("SectionId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.UserRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Warehouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.WarehouseInvoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsPayed");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("RestaurantId");

                    b.Property<long>("SupplierId");

                    b.Property<long>("UserId");

                    b.Property<long>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseInvoices");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Food", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Foods")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodGroup", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("FoodGroups")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodGroupRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.FoodGroup", "FoodGroup")
                        .WithMany("FoodGroupRels")
                        .HasForeignKey("FoodGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Food", "Food")
                        .WithMany("FoodGroupRels")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Ingredient", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Ingredients")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientCategory", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("IngredientCategories")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientCategoryRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.IngredientCategory", "IngredientCategory")
                        .WithMany("IngredientCategoryRels")
                        .HasForeignKey("IngredientCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Ingredient", "Ingredient")
                        .WithMany("IngredientCategoryRels")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientFoodRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Food", "Food")
                        .WithMany("IngredientFoodRels")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientsInsert", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.WarehouseInvoice", "WarehouseInvoice")
                        .WithMany("IngredientsInserts")
                        .HasForeignKey("WarehouseInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestWareRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("WareHouses")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Warehouse", "Warehouse")
                        .WithMany("RestWareRels")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Restaurant", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Branch")
                        .WithMany("Branches")
                        .HasForeignKey("BranchId");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Role", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Roles")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Section", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Sections")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Supplier", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Suppliers")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Table", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Section", "Section")
                        .WithMany("Tables")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.User", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Users")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.UserRole", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.WarehouseInvoice", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("WarehouseInvoices")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

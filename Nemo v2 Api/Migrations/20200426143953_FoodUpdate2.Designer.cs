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
    [Migration("20200426143953_FoodUpdate2")]
    partial class FoodUpdate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Nemo_v2_Data.Entities.Buyer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Food", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<long?>("PrinterId");

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("PrinterId");

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

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodInvoiceRel", b =>
                {
                    b.Property<long>("FoodId");

                    b.Property<long>("InvoiceId");

                    b.HasKey("FoodId", "InvoiceId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("FoodInvoiceRel");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodPrinterAndSectionRel", b =>
                {
                    b.Property<long>("FoodId");

                    b.Property<long>("PrinterId");

                    b.Property<long>("SectionId");

                    b.HasKey("FoodId", "PrinterId", "SectionId");

                    b.HasIndex("PrinterId");

                    b.HasIndex("SectionId");

                    b.ToTable("FoodPrinterAndSectionRel");
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

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

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

                    b.Property<decimal>("Quantity");

                    b.Property<int>("Unit");

                    b.HasKey("FoodId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientFoodRel");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientWarehouseRel", b =>
                {
                    b.Property<long>("WarehouseId");

                    b.Property<long>("IngredientId");

                    b.Property<decimal>("Quantity");

                    b.HasKey("WarehouseId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientWarehouseRels");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientsExport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("IngredientId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("PriceForEach");

                    b.Property<decimal>("Quantity");

                    b.Property<long>("RestaurantId");

                    b.Property<long>("WarehouseExportInvoiceId");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("WarehouseExportInvoiceId");

                    b.ToTable("IngredientsExports");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientsInsert", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("IngredientId");

                    b.Property<decimal>("MinimumQuantity");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Quantity");

                    b.Property<long>("RestaurantId");

                    b.Property<int>("Unit");

                    b.Property<long>("WarehouseInvoiceId");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("WarehouseInvoiceId");

                    b.ToTable("IngredientsInserts");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Invoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("Discount");

                    b.Property<int>("InvoiceType");

                    b.Property<bool>("IsIngredientReducted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("RestaurantId");

                    b.Property<decimal>("ServiceCharge");

                    b.Property<long>("TableId");

                    b.Property<decimal>("TotalAmount");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Printer", b =>
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

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Profit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("IngredientProfit");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("ProductProfit");

                    b.Property<long>("RestaurantId");

                    b.Property<decimal>("VAT");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("Profits");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestBuyerRel", b =>
                {
                    b.Property<long>("RestaurantId");

                    b.Property<long>("BuyerId");

                    b.HasKey("RestaurantId", "BuyerId");

                    b.HasIndex("BuyerId");

                    b.ToTable("RestBuyerRels");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestSupplierRel", b =>
                {
                    b.Property<long>("RestaurantId");

                    b.Property<long>("SupplierId");

                    b.HasKey("RestaurantId", "SupplierId");

                    b.HasIndex("SupplierId");

                    b.ToTable("RestSupplierRels");
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

                    b.HasKey("Id");

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

                    b.HasIndex("Password", "RestaurantId")
                        .IsUnique();

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

            modelBuilder.Entity("Nemo_v2_Data.Entities.WarehouseExportInvoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("BuyerId");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsPayed");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("RestaurantId");

                    b.Property<decimal>("TotalAmount");

                    b.Property<long>("UserId");

                    b.Property<long>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseExportInvoices");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.WarehouseInvoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("ComputedNumber");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Discount");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired();

                    b.Property<bool>("IsPayed");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Note");

                    b.Property<DateTime>("PromisedDateTime");

                    b.Property<string>("ResponsiblePerson")
                        .IsRequired();

                    b.Property<long>("RestaurantId");

                    b.Property<string>("SupplierAdress")
                        .IsRequired();

                    b.Property<long>("SupplierId");

                    b.Property<decimal>("TotalAmount");

                    b.Property<long>("UserId");

                    b.Property<decimal>("VAT");

                    b.Property<string>("ValuteCode")
                        .IsRequired();

                    b.Property<string>("ValuteValue")
                        .IsRequired();

                    b.Property<long>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarehouseId");

                    b.HasIndex("ComputedNumber", "RestaurantId")
                        .IsUnique();

                    b.ToTable("WarehouseInvoices");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Food", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Printer")
                        .WithMany("Foods")
                        .HasForeignKey("PrinterId");

                    b.HasOne("Nemo_v2_Data.Entities.Restaurant")
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
                        .WithMany("FoodGroups")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodInvoiceRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Invoice", "Invoice")
                        .WithMany("Foods")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.FoodPrinterAndSectionRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Food", "Food")
                        .WithMany("FoodPrinterAndSectionRels")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Printer", "Printer")
                        .WithMany()
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Ingredient", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Ingredients")
                        .HasForeignKey("RestaurantId")
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
                        .WithMany("IngredientCategories")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientFoodRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Food", "Food")
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientWarehouseRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Ingredient", "Ingredient")
                        .WithMany("IngredientWarehouseRels")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Warehouse", "Warehouse")
                        .WithMany("IngredientWarehouseRels")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.IngredientsExport", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.WarehouseExportInvoice", "WarehouseExportInvoice")
                        .WithMany("IngredientsExports")
                        .HasForeignKey("WarehouseExportInvoiceId")
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

            modelBuilder.Entity("Nemo_v2_Data.Entities.Invoice", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Printer", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Profit", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestBuyerRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Buyer", "Buyer")
                        .WithMany("RestBuyerRels")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestSupplierRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany("Supplliers")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Supplier", "Supplier")
                        .WithMany("RestSupplierRels")
                        .HasForeignKey("SupplierId")
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

            modelBuilder.Entity("Nemo_v2_Data.Entities.WarehouseExportInvoice", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
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

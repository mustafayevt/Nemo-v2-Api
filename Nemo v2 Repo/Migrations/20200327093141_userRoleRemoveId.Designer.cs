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
    [Migration("20200327093141_userRoleRemoveId")]
    partial class userRoleRemoveId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Nemo_v2_Data.Entities.Ingredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("IngredientCategoryId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("IngredientCategoryId");

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

                    b.HasKey("Id");

                    b.ToTable("IngredientCategories");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestWareRel", b =>
                {
                    b.Property<long>("RestaurantId");

                    b.Property<long>("WarehouseId");

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("Id");

                    b.Property<DateTime>("ModifiedDate");

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

            modelBuilder.Entity("Nemo_v2_Data.Entities.Table", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("SectionId");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Nemo_v2_Data.Entities.Ingredient", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.IngredientCategory", "IngredientCategory")
                        .WithMany()
                        .HasForeignKey("IngredientCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.RestWareRel", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nemo_v2_Data.Entities.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Restaurant", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId");
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Role", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Section", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.Table", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nemo_v2_Data.Entities.User", b =>
                {
                    b.HasOne("Nemo_v2_Data.Entities.Restaurant", "Restaurant")
                        .WithMany()
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
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class addRestIdAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 2147483647, nullable: false),
                    BranchId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Restaurants_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 2147483647, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodGroups_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientCategories_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RestaurantId = table.Column<long>(nullable: false),
                    CanFinishDay = table.Column<bool>(nullable: false),
                    CanAddProduct = table.Column<bool>(nullable: false),
                    CanVoidProduct = table.Column<bool>(nullable: false),
                    CanTransferInvoice = table.Column<bool>(nullable: false),
                    CanMergeInvoices = table.Column<bool>(nullable: false),
                    CanCloseCheck = table.Column<bool>(nullable: false),
                    CanInsertIngredient = table.Column<bool>(nullable: false),
                    CanShowStock = table.Column<bool>(nullable: false),
                    CanShowProductTransfers = table.Column<bool>(nullable: false),
                    CanExit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestWareRels",
                columns: table => new
                {
                    RestaurantId = table.Column<long>(nullable: false),
                    WarehouseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestWareRels", x => new { x.RestaurantId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_RestWareRels_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestWareRels_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    FoodGroupId = table.Column<long>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_FoodGroups_FoodGroupId",
                        column: x => x.FoodGroupId,
                        principalTable: "FoodGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foods_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    WarehouseId = table.Column<long>(nullable: false),
                    IngredientCategoryId = table.Column<long>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientCategories_IngredientCategoryId",
                        column: x => x.IngredientCategoryId,
                        principalTable: "IngredientCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SectionId = table.Column<long>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tables_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInvoices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    InvoiceNumber = table.Column<string>(maxLength: 50, nullable: false),
                    SupplierId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    WarehouseId = table.Column<long>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    IsPayed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseInvoices_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInvoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInvoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInvoices_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientFoodRel",
                columns: table => new
                {
                    FoodId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientFoodRel", x => new { x.FoodId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientFoodRel_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientFoodRel_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsInserts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    WarehouseInvoiceId = table.Column<long>(nullable: false),
                    InitialCount = table.Column<decimal>(nullable: false),
                    CurrentCount = table.Column<decimal>(nullable: false),
                    PriceForEach = table.Column<decimal>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsInserts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsInserts_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsInserts_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsInserts_WarehouseInvoices_WarehouseInvoiceId",
                        column: x => x.WarehouseInvoiceId,
                        principalTable: "WarehouseInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodGroups_RestaurantId",
                table: "FoodGroups",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodGroupId",
                table: "Foods",
                column: "FoodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RestaurantId",
                table: "Foods",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientCategories_RestaurantId",
                table: "IngredientCategories",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientFoodRel_IngredientId",
                table: "IngredientFoodRel",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientCategoryId",
                table: "Ingredients",
                column: "IngredientCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RestaurantId",
                table: "Ingredients",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_WarehouseId",
                table: "Ingredients",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsInserts_IngredientId",
                table: "IngredientsInserts",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsInserts_RestaurantId",
                table: "IngredientsInserts",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsInserts_WarehouseInvoiceId",
                table: "IngredientsInserts",
                column: "WarehouseInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_BranchId",
                table: "Restaurants",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RestWareRels_WarehouseId",
                table: "RestWareRels",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RestaurantId",
                table: "Roles",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_RestaurantId",
                table: "Sections",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_RestaurantId",
                table: "Suppliers",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_SectionId",
                table: "Tables",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RestaurantId",
                table: "Users",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_RestaurantId",
                table: "WarehouseInvoices",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_SupplierId",
                table: "WarehouseInvoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_UserId",
                table: "WarehouseInvoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_WarehouseId",
                table: "WarehouseInvoices",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientFoodRel");

            migrationBuilder.DropTable(
                name: "IngredientsInserts");

            migrationBuilder.DropTable(
                name: "RestWareRels");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "WarehouseInvoices");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "FoodGroups");

            migrationBuilder.DropTable(
                name: "IngredientCategories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}

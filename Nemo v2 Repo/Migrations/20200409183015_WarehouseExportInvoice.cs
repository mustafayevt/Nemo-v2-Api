using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseExportInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseExportInvoices",
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
                    table.PrimaryKey("PK_WarehouseExportInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseExportInvoices_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseExportInvoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseExportInvoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseExportInvoices_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestBuyerRels",
                columns: table => new
                {
                    RestaurantId = table.Column<long>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestBuyerRels", x => new { x.RestaurantId, x.BuyerId });
                    table.ForeignKey(
                        name: "FK_RestBuyerRels_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestBuyerRels_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsExports",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    WarehouseExportInvoiceId = table.Column<long>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    PriceForEach = table.Column<decimal>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsExports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsExports_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsExports_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsExports_WarehouseExportInvoices_WarehouseExportI~",
                        column: x => x.WarehouseExportInvoiceId,
                        principalTable: "WarehouseExportInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsExports_IngredientId",
                table: "IngredientsExports",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsExports_RestaurantId",
                table: "IngredientsExports",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsExports_WarehouseExportInvoiceId",
                table: "IngredientsExports",
                column: "WarehouseExportInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RestBuyerRels_BuyerId",
                table: "RestBuyerRels",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseExportInvoices_RestaurantId",
                table: "WarehouseExportInvoices",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseExportInvoices_SupplierId",
                table: "WarehouseExportInvoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseExportInvoices_UserId",
                table: "WarehouseExportInvoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseExportInvoices_WarehouseId",
                table: "WarehouseExportInvoices",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsExports");

            migrationBuilder.DropTable(
                name: "RestBuyerRels");

            migrationBuilder.DropTable(
                name: "WarehouseExportInvoices");

            migrationBuilder.DropTable(
                name: "Buyers");
        }
    }
}

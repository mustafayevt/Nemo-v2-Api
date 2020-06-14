using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class IngredientWarehouseRelUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Ingredients_IngredientId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseTransferInvoices_IngredientId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "PriceForEach",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "WarehouseTransferInvoices");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "IngredientWarehouseRels",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateTable(
                name: "IngredientsTransfer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    WarehouseTransferInvoiceId = table.Column<long>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    PriceForEach = table.Column<decimal>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    Unit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsTransfer_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsTransfer_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsTransfer_WarehouseTransferInvoices_WarehouseTran~",
                        column: x => x.WarehouseTransferInvoiceId,
                        principalTable: "WarehouseTransferInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTransfer_IngredientId",
                table: "IngredientsTransfer",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTransfer_RestaurantId",
                table: "IngredientsTransfer",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTransfer_WarehouseTransferInvoiceId",
                table: "IngredientsTransfer",
                column: "WarehouseTransferInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsTransfer");

            migrationBuilder.AddColumn<long>(
                name: "IngredientId",
                table: "WarehouseTransferInvoices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceForEach",
                table: "WarehouseTransferInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "WarehouseTransferInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "IngredientWarehouseRels",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_IngredientId",
                table: "WarehouseTransferInvoices",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Ingredients_IngredientId",
                table: "WarehouseTransferInvoices",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

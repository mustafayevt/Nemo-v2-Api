using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class warehouseTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseTransferInvoices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    InvoiceNumber = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    WarehouseId = table.Column<long>(nullable: false),
                    AcceptorWarehouseId = table.Column<long>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    IsPayed = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseTransferInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseTransferInvoices_Warehouses_AcceptorWarehouseId",
                        column: x => x.AcceptorWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseTransferInvoices_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseTransferInvoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseTransferInvoices_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsTransfers",
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
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsTransfers_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsTransfers_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsTransfers_WarehouseTransferInvoices_WarehouseTra~",
                        column: x => x.WarehouseTransferInvoiceId,
                        principalTable: "WarehouseTransferInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTransfers_IngredientId",
                table: "IngredientsTransfers",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTransfers_RestaurantId",
                table: "IngredientsTransfers",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTransfers_WarehouseTransferInvoiceId",
                table: "IngredientsTransfers",
                column: "WarehouseTransferInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_AcceptorWarehouseId",
                table: "WarehouseTransferInvoices",
                column: "AcceptorWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_RestaurantId",
                table: "WarehouseTransferInvoices",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_UserId",
                table: "WarehouseTransferInvoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_WarehouseId",
                table: "WarehouseTransferInvoices",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsTransfers");

            migrationBuilder.DropTable(
                name: "WarehouseTransferInvoices");
        }
    }
}

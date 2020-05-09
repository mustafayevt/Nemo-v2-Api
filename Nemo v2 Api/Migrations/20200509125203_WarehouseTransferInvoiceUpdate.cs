using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseTransferInvoiceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Users_UserId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Warehouses_WarehouseId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropTable(
                name: "IngredientsTransfers");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "WarehouseTransferInvoices",
                newName: "RequesterWarehouseId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WarehouseTransferInvoices",
                newName: "RequesterUserId");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "WarehouseTransferInvoices",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseTransferInvoices_WarehouseId",
                table: "WarehouseTransferInvoices",
                newName: "IX_WarehouseTransferInvoices_RequesterWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseTransferInvoices_UserId",
                table: "WarehouseTransferInvoices",
                newName: "IX_WarehouseTransferInvoices_RequesterUserId");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "WarehouseTransferInvoices",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedTime",
                table: "WarehouseTransferInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "AcceptorUserId",
                table: "WarehouseTransferInvoices",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedTime",
                table: "WarehouseTransferInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_AcceptorUserId",
                table: "WarehouseTransferInvoices",
                column: "AcceptorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseTransferInvoices_IngredientId",
                table: "WarehouseTransferInvoices",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Users_AcceptorUserId",
                table: "WarehouseTransferInvoices",
                column: "AcceptorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Ingredients_IngredientId",
                table: "WarehouseTransferInvoices",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Users_RequesterUserId",
                table: "WarehouseTransferInvoices",
                column: "RequesterUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Warehouses_RequesterWarehouseId",
                table: "WarehouseTransferInvoices",
                column: "RequesterWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Users_AcceptorUserId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Ingredients_IngredientId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Users_RequesterUserId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseTransferInvoices_Warehouses_RequesterWarehouseId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseTransferInvoices_AcceptorUserId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseTransferInvoices_IngredientId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "AcceptedTime",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "AcceptorUserId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "PriceForEach",
                table: "WarehouseTransferInvoices");

            migrationBuilder.DropColumn(
                name: "RequestedTime",
                table: "WarehouseTransferInvoices");

            migrationBuilder.RenameColumn(
                name: "RequesterWarehouseId",
                table: "WarehouseTransferInvoices",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "RequesterUserId",
                table: "WarehouseTransferInvoices",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "WarehouseTransferInvoices",
                newName: "TotalAmount");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseTransferInvoices_RequesterWarehouseId",
                table: "WarehouseTransferInvoices",
                newName: "IX_WarehouseTransferInvoices_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseTransferInvoices_RequesterUserId",
                table: "WarehouseTransferInvoices",
                newName: "IX_WarehouseTransferInvoices_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "WarehouseTransferInvoices",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "IngredientsTransfers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    PriceForEach = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    WarehouseTransferInvoiceId = table.Column<long>(nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Users_UserId",
                table: "WarehouseTransferInvoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseTransferInvoices_Warehouses_WarehouseId",
                table: "WarehouseTransferInvoices",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

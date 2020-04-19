using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class IngredientWarehouseRels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Warehouses_WarehouseId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_WarehouseId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "CurrentQuantity",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientWarehouseRels",
                columns: table => new
                {
                    WarehouseId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientWarehouseRels", x => new { x.WarehouseId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientWarehouseRels_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientWarehouseRels_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWarehouseRels_IngredientId",
                table: "IngredientWarehouseRels",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientWarehouseRels");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentQuantity",
                table: "Ingredients",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "Ingredients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_WarehouseId",
                table: "Ingredients",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Warehouses_WarehouseId",
                table: "Ingredients",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

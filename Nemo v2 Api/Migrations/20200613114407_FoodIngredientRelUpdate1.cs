using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodIngredientRelUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "IngredientFoodRel",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<short>(
                name: "Count",
                table: "FoodInvoiceProperties",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientFoodRel_WarehouseId",
                table: "IngredientFoodRel",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientFoodRel_Warehouses_WarehouseId",
                table: "IngredientFoodRel",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientFoodRel_Warehouses_WarehouseId",
                table: "IngredientFoodRel");

            migrationBuilder.DropIndex(
                name: "IX_IngredientFoodRel_WarehouseId",
                table: "IngredientFoodRel");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "IngredientFoodRel");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "FoodInvoiceProperties");
        }
    }
}

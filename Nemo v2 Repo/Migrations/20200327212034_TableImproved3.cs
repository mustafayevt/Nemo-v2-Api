using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class TableImproved3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Foods_Ingredient",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Ingredient",
                table: "Ingredients",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_Ingredient",
                table: "Ingredients",
                newName: "IX_Ingredients_FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Foods_FoodId",
                table: "Ingredients",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Foods_FoodId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_FoodId",
                table: "Ingredients",
                newName: "IX_Ingredients_Ingredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Foods_Ingredient",
                table: "Ingredients",
                column: "Ingredient",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

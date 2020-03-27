using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class TableImproved1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Foods_FoodId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "Ingredients",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_FoodId",
                table: "Ingredients",
                newName: "IX_Ingredients_Ingredients");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Foods_Ingredients",
                table: "Ingredients",
                column: "Ingredients",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Foods_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Ingredients",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_Ingredients",
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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class TableImproved4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RestaurantId",
                table: "IngredientCategories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientCategories_RestaurantId",
                table: "IngredientCategories",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientCategories_Restaurants_RestaurantId",
                table: "IngredientCategories",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientCategories_Restaurants_RestaurantId",
                table: "IngredientCategories");

            migrationBuilder.DropIndex(
                name: "IX_IngredientCategories_RestaurantId",
                table: "IngredientCategories");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "IngredientCategories");
        }
    }
}

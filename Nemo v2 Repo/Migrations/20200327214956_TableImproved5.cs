using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class TableImproved5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Foods_FoodId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_FoodId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Ingredients");

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

            migrationBuilder.CreateIndex(
                name: "IX_IngredientFoodRel_IngredientId",
                table: "IngredientFoodRel",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientFoodRel");

            migrationBuilder.AddColumn<long>(
                name: "FoodId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_FoodId",
                table: "Ingredients",
                column: "FoodId");

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

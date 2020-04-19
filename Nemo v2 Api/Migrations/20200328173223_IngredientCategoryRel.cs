using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class IngredientCategoryRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientCategories_IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientCategoryRel",
                columns: table => new
                {
                    IngredientId = table.Column<long>(nullable: false),
                    IngredientCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientCategoryRel", x => new { x.IngredientId, x.IngredientCategoryId });
                    table.ForeignKey(
                        name: "FK_IngredientCategoryRel_IngredientCategories_IngredientCatego~",
                        column: x => x.IngredientCategoryId,
                        principalTable: "IngredientCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientCategoryRel_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientCategoryRel_IngredientCategoryId",
                table: "IngredientCategoryRel",
                column: "IngredientCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientCategoryRel");

            migrationBuilder.AddColumn<long>(
                name: "IngredientCategoryId",
                table: "Ingredients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientCategoryId",
                table: "Ingredients",
                column: "IngredientCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientCategories_IngredientCategoryId",
                table: "Ingredients",
                column: "IngredientCategoryId",
                principalTable: "IngredientCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

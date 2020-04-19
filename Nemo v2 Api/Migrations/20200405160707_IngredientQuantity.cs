using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class IngredientQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCount",
                table: "IngredientsInserts");

            migrationBuilder.RenameColumn(
                name: "InitialCount",
                table: "IngredientsInserts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "CurrentCount",
                table: "Ingredients",
                newName: "CurrentQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "IngredientsInserts",
                newName: "InitialCount");

            migrationBuilder.RenameColumn(
                name: "CurrentQuantity",
                table: "Ingredients",
                newName: "CurrentCount");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentCount",
                table: "IngredientsInserts",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

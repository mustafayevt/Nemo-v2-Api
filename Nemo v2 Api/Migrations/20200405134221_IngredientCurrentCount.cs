using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class IngredientCurrentCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:invoice_type", "normal,gift,not_paid");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentCount",
                table: "Ingredients",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCount",
                table: "Ingredients");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:invoice_type", "normal,gift,not_paid");
        }
    }
}

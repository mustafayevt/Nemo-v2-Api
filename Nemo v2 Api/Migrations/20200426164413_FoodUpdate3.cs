using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Printers_PrinterId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_PrinterId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "Foods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrinterId",
                table: "Foods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_PrinterId",
                table: "Foods",
                column: "PrinterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Printers_PrinterId",
                table: "Foods",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

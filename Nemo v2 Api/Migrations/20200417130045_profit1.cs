using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class profit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profits_RestaurantId",
                table: "Profits");

            migrationBuilder.CreateIndex(
                name: "IX_Profits_RestaurantId",
                table: "Profits",
                column: "RestaurantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profits_RestaurantId",
                table: "Profits");

            migrationBuilder.CreateIndex(
                name: "IX_Profits_RestaurantId",
                table: "Profits",
                column: "RestaurantId");
        }
    }
}

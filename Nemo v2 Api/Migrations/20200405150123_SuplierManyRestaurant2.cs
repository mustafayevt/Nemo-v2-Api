using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class SuplierManyRestaurant2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Restaurants_RestaurantId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_RestaurantId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Suppliers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RestaurantId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_RestaurantId",
                table: "Suppliers",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Restaurants_RestaurantId",
                table: "Suppliers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

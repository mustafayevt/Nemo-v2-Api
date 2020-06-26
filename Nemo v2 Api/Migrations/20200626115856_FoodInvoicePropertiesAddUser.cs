using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodInvoicePropertiesAddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "FoodInvoiceProperties",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceProperties_UserId",
                table: "FoodInvoiceProperties",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodInvoiceProperties_Users_UserId",
                table: "FoodInvoiceProperties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodInvoiceProperties_Users_UserId",
                table: "FoodInvoiceProperties");

            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceProperties_UserId",
                table: "FoodInvoiceProperties");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodInvoiceProperties");
        }
    }
}

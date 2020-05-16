using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodInoviceRelMapUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceRel_FoodId",
                table: "FoodInvoiceRel");

            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceRel_Id",
                table: "FoodInvoiceRel");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_FoodId_InvoiceId",
                table: "FoodInvoiceRel",
                columns: new[] { "FoodId", "InvoiceId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceRel_FoodId_InvoiceId",
                table: "FoodInvoiceRel");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_FoodId",
                table: "FoodInvoiceRel",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_Id",
                table: "FoodInvoiceRel",
                column: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodInoviceRelMap4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel");

            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceRel_FoodId",
                table: "FoodInvoiceRel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel",
                columns: new[] { "FoodId", "InvoiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_InvoiceId",
                table: "FoodInvoiceRel",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel");

            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceRel_InvoiceId",
                table: "FoodInvoiceRel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_FoodId",
                table: "FoodInvoiceRel",
                column: "FoodId");
        }
    }
}

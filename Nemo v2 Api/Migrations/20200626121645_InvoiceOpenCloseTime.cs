using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class InvoiceOpenCloseTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClosedTime",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenedTime",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TableId",
                table: "FoodInvoiceProperties",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceProperties_TableId",
                table: "FoodInvoiceProperties",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodInvoiceProperties_Tables_TableId",
                table: "FoodInvoiceProperties",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodInvoiceProperties_Tables_TableId",
                table: "FoodInvoiceProperties");

            migrationBuilder.DropIndex(
                name: "IX_FoodInvoiceProperties_TableId",
                table: "FoodInvoiceProperties");

            migrationBuilder.DropColumn(
                name: "ClosedTime",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OpenedTime",
                table: "Invoices");

            migrationBuilder.AlterColumn<decimal>(
                name: "TableId",
                table: "FoodInvoiceProperties",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}

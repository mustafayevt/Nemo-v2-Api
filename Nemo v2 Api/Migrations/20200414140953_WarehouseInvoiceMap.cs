using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseInvoiceMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "WarehouseInvoices",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "ComputedNumber",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices",
                columns: new[] { "ComputedNumber", "WarehouseId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "ComputedNumber",
                table: "WarehouseInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "WarehouseInvoices",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

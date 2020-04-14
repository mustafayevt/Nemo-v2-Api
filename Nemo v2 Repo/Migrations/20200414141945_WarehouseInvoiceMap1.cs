using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseInvoiceMap1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "ComputedNumber",
                table: "WarehouseInvoices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

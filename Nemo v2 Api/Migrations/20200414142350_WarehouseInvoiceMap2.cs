using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseInvoiceMap2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ComputedNumber",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: 0L);

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
        }
    }
}

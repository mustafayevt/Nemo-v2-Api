using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseExportInvoice1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseExportInvoices_Suppliers_SupplierId",
                table: "WarehouseExportInvoices");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "WarehouseExportInvoices",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseExportInvoices_SupplierId",
                table: "WarehouseExportInvoices",
                newName: "IX_WarehouseExportInvoices_BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseExportInvoices_Buyers_BuyerId",
                table: "WarehouseExportInvoices",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseExportInvoices_Buyers_BuyerId",
                table: "WarehouseExportInvoices");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "WarehouseExportInvoices",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseExportInvoices_BuyerId",
                table: "WarehouseExportInvoices",
                newName: "IX_WarehouseExportInvoices_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseExportInvoices_Suppliers_SupplierId",
                table: "WarehouseExportInvoices",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

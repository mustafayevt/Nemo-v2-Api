using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class InvoiceNumberProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastInvoiceNumber",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LastWarehouseExportInvoiceNumber",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LastWarehouseInsertInvoiceNumber",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LastWarehouseTransferInvoiceNumber",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastInvoiceNumber",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LastWarehouseExportInvoiceNumber",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LastWarehouseInsertInvoiceNumber",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LastWarehouseTransferInvoiceNumber",
                table: "Restaurants");
        }
    }
}

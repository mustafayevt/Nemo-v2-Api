using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class updateWarehouseAndINserts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceForEach",
                table: "IngredientsInserts",
                newName: "Price");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePerson",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupplierAdress",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ValuteCode",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValuteValue",
                table: "WarehouseInvoices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumQuantity",
                table: "IngredientsInserts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "IngredientsInserts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "ResponsiblePerson",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "SupplierAdress",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "ValuteCode",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "ValuteValue",
                table: "WarehouseInvoices");

            migrationBuilder.DropColumn(
                name: "MinimumQuantity",
                table: "IngredientsInserts");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "IngredientsInserts");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "IngredientsInserts",
                newName: "PriceForEach");
        }
    }
}

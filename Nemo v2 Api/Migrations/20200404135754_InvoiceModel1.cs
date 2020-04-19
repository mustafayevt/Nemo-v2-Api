using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class InvoiceModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Invoices_InvoiceId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_InvoiceId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Foods");

            migrationBuilder.AddColumn<List<long>>(
                name: "FoodIds",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodIds",
                table: "Invoices");

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "Foods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_InvoiceId",
                table: "Foods",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Invoices_InvoiceId",
                table: "Foods",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

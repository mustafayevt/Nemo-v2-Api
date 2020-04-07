using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodInvoiceRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodIds",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "FoodInvoiceRel",
                columns: table => new
                {
                    FoodId = table.Column<long>(nullable: false),
                    InvoiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodInvoiceRel", x => new { x.FoodId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_FoodInvoiceRel_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodInvoiceRel_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_InvoiceId",
                table: "FoodInvoiceRel",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodInvoiceRel");

            migrationBuilder.AddColumn<List<long>>(
                name: "FoodIds",
                table: "Invoices",
                nullable: true);
        }
    }
}

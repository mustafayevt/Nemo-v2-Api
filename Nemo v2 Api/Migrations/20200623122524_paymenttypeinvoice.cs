using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class paymenttypeinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardPayment",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CashPayment",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentType_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypeInvoiceRel",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(nullable: false),
                    PaymentTypeId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeInvoiceRel", x => new { x.PaymentTypeId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_PaymentTypeInvoiceRel_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTypeInvoiceRel_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentType_RestaurantId",
                table: "PaymentType",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypeInvoiceRel_InvoiceId",
                table: "PaymentTypeInvoiceRel",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTypeInvoiceRel");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.AddColumn<decimal>(
                name: "CardPayment",
                table: "Invoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashPayment",
                table: "Invoices",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

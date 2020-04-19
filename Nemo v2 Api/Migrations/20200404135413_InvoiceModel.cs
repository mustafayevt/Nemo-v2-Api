using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class InvoiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "Foods",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    InvoiceType = table.Column<int>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    TableId = table.Column<long>(nullable: false),
                    IsIngredientReducted = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    ServiceCharge = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_InvoiceId",
                table: "Foods",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RestaurantId",
                table: "Invoices",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TableId",
                table: "Invoices",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Invoices_InvoiceId",
                table: "Foods",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Invoices_InvoiceId",
                table: "Foods");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Foods_InvoiceId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Foods");
        }
    }
}

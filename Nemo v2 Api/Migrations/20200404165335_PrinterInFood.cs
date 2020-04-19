using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class PrinterInFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrinterId",
                table: "Foods",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printers_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_PrinterId",
                table: "Foods",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_RestaurantId",
                table: "Printers",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Printers_PrinterId",
                table: "Foods",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Printers_PrinterId",
                table: "Foods");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Foods_PrinterId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "Foods");
        }
    }
}

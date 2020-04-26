using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Foods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Foods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "FoodPrinterAndSectionRel",
                columns: table => new
                {
                    FoodId = table.Column<long>(nullable: false),
                    PrinterId = table.Column<long>(nullable: false),
                    SectionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPrinterAndSectionRel", x => new { x.FoodId, x.PrinterId, x.SectionId });
                    table.ForeignKey(
                        name: "FK_FoodPrinterAndSectionRel_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodPrinterAndSectionRel_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodPrinterAndSectionRel_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPrinterAndSectionRel_PrinterId",
                table: "FoodPrinterAndSectionRel",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPrinterAndSectionRel_SectionId",
                table: "FoodPrinterAndSectionRel",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodPrinterAndSectionRel");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Foods");
        }
    }
}

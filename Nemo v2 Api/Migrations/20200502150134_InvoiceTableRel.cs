using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class InvoiceTableRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Tables_TableId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_TableId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "InvoiceTableRel",
                columns: table => new
                {
                    TableId = table.Column<long>(nullable: false),
                    InvoiceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTableRel", x => new { x.TableId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_InvoiceTableRel_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceTableRel_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTableRel_InvoiceId",
                table: "InvoiceTableRel",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceTableRel");

            migrationBuilder.AddColumn<long>(
                name: "TableId",
                table: "Invoices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TableId",
                table: "Invoices",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Tables_TableId",
                table: "Invoices",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

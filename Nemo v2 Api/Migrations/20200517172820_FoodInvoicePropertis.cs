using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodInvoicePropertis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "FoodInvoiceProperties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FoodSaleType = table.Column<int>(nullable: false),
                    OriginalPrice = table.Column<decimal>(nullable: false),
                    ChangedPrice = table.Column<decimal>(nullable: false),
                    FoodInvoiceRelFoodId = table.Column<long>(nullable: true),
                    FoodInvoiceRelInvoiceId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodInvoiceProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodInvoiceProperties_FoodInvoiceRel_FoodInvoiceRelFoodId_F~",
                        columns: x => new { x.FoodInvoiceRelFoodId, x.FoodInvoiceRelInvoiceId },
                        principalTable: "FoodInvoiceRel",
                        principalColumns: new[] { "FoodId", "InvoiceId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceProperties_FoodInvoiceRelFoodId_FoodInvoiceRelIn~",
                table: "FoodInvoiceProperties",
                columns: new[] { "FoodInvoiceRelFoodId", "FoodInvoiceRelInvoiceId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodInvoiceProperties");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);
        }
    }
}

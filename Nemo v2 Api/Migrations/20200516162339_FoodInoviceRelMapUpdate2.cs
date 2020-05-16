using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodInoviceRelMapUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel");

           

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FoodInvoiceRel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "FoodInvoiceRel",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodInvoiceRel",
                table: "FoodInvoiceRel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInvoiceRel_InvoiceId",
                table: "FoodInvoiceRel",
                column: "InvoiceId");
        }
    }
}

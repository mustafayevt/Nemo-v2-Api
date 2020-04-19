using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseInvoiceMap3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices");

            migrationBuilder.AlterColumn<long>(
                name: "ComputedNumber",
                table: "WarehouseInvoices",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ComputedNumber",
                table: "WarehouseInvoices",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices",
                columns: new[] { "ComputedNumber", "WarehouseId" },
                unique: true);
        }
    }
}

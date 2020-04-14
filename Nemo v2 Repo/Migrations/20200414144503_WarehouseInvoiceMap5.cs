using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Nemo_v2_Repo.Migrations
{
    public partial class WarehouseInvoiceMap5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ComputedNumber",
                table: "WarehouseInvoices",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices",
                columns: new[] { "ComputedNumber", "WarehouseId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseInvoices_ComputedNumber_WarehouseId",
                table: "WarehouseInvoices");

            migrationBuilder.AlterColumn<long>(
                name: "ComputedNumber",
                table: "WarehouseInvoices",
                nullable: true,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class rolePermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAddProduct",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanCloseCheck",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanExit",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanFinishDay",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanInsertIngredient",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanMergeInvoices",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanShowProductTransfers",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanShowStock",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanTransferInvoice",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanVoidProduct",
                table: "Roles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAddProduct",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanCloseCheck",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanExit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanFinishDay",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanInsertIngredient",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanMergeInvoices",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanShowProductTransfers",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanShowStock",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanTransferInvoice",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanVoidProduct",
                table: "Roles");
        }
    }
}

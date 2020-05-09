using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class InvoiceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_UserId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Invoices",
                newName: "SectionId");

            migrationBuilder.RenameColumn(
                name: "IsIngredientReducted",
                table: "Invoices",
                newName: "IsIngredientReduced");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                newName: "IX_Invoices_SectionId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Invoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "ClosedUserId",
                table: "Invoices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OpenedUserId",
                table: "Invoices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClosedUserId",
                table: "Invoices",
                column: "ClosedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OpenedUserId",
                table: "Invoices",
                column: "OpenedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_ClosedUserId",
                table: "Invoices",
                column: "ClosedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_OpenedUserId",
                table: "Invoices",
                column: "OpenedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Sections_SectionId",
                table: "Invoices",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_ClosedUserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_OpenedUserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Sections_SectionId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ClosedUserId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_OpenedUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ClosedUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OpenedUserId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Invoices",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IsIngredientReduced",
                table: "Invoices",
                newName: "IsIngredientReducted");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SectionId",
                table: "Invoices",
                newName: "IX_Invoices_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_UserId",
                table: "Invoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

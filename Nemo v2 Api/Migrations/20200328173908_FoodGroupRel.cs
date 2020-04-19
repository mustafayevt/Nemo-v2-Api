using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class FoodGroupRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodGroups_FoodGroupId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodGroupId",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "FoodGroupRel",
                columns: table => new
                {
                    FoodId = table.Column<long>(nullable: false),
                    FoodGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodGroupRel", x => new { x.FoodId, x.FoodGroupId });
                    table.ForeignKey(
                        name: "FK_FoodGroupRel_FoodGroups_FoodGroupId",
                        column: x => x.FoodGroupId,
                        principalTable: "FoodGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodGroupRel_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodGroupRel_FoodGroupId",
                table: "FoodGroupRel",
                column: "FoodGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodGroupRel");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodGroupId",
                table: "Foods",
                column: "FoodGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodGroups_FoodGroupId",
                table: "Foods",
                column: "FoodGroupId",
                principalTable: "FoodGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Nemo_v2_Repo.Migrations
{
    public partial class SuplierManyRestaurant1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Restaurants_RestaurantId",
                table: "Suppliers");

            migrationBuilder.AlterColumn<long>(
                name: "RestaurantId",
                table: "Suppliers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateTable(
                name: "RestSupplierRels",
                columns: table => new
                {
                    RestaurantId = table.Column<long>(nullable: false),
                    SupplierId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestSupplierRels", x => new { x.RestaurantId, x.SupplierId });
                    table.ForeignKey(
                        name: "FK_RestSupplierRels_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestSupplierRels_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestSupplierRels_SupplierId",
                table: "RestSupplierRels",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Restaurants_RestaurantId",
                table: "Suppliers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Restaurants_RestaurantId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "RestSupplierRels");

            migrationBuilder.AlterColumn<long>(
                name: "RestaurantId",
                table: "Suppliers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Restaurants_RestaurantId",
                table: "Suppliers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdirneTravel.Migrations
{
    /// <inheritdoc />
    public partial class TravelRouteCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Category_CategoryId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_CategoryId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_CategoryId",
                table: "Routes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Category_CategoryId",
                table: "Routes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Category_CategoryId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_CategoryId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Places_CategoryId",
                table: "Places",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Category_CategoryId",
                table: "Places",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

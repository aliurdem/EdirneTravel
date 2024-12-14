using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdirneTravel.Migrations
{
    /// <inheritdoc />
    public partial class PlaceUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntranceFee",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocationInfo",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisitableHours",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntranceFee",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "LocationInfo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "VisitableHours",
                table: "Places");
        }
    }
}

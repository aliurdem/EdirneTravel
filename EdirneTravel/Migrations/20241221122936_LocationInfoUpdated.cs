using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdirneTravel.Migrations
{
    /// <inheritdoc />
    public partial class LocationInfoUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationInfo",
                table: "Places",
                newName: "Longitude");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Places",
                newName: "LocationInfo");
        }
    }
}

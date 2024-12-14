using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdirneTravel.Migrations
{
    /// <inheritdoc />
    public partial class TravelRouteUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AverageDuration",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Routes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageDuration",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Routes");
        }
    }
}

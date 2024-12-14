using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdirneTravel.Migrations
{
    /// <inheritdoc />
    public partial class imageDataPropAddedToPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Places",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Places");
        }
    }
}

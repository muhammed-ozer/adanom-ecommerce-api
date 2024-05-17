using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLogoPathColumnOnBrandTableAndAddImageTypeToImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Brands");

            migrationBuilder.AddColumn<byte>(
                name: "ImageType",
                table: "Images",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Brands",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}

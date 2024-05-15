using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeColumnToAddressCitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Code",
                table: "AddressCities",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AddressCities");
        }
    }
}

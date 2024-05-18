using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeletedColumnsOnImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedByUserId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}

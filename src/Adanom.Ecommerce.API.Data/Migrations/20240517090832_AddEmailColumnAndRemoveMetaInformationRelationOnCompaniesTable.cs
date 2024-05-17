using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailColumnAndRemoveMetaInformationRelationOnCompaniesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_MetaInformations_MetaInformationId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_MetaInformationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MetaInformationId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.AddColumn<long>(
                name: "MetaInformationId",
                table: "Companies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_MetaInformationId",
                table: "Companies",
                column: "MetaInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_MetaInformations_MetaInformationId",
                table: "Companies",
                column: "MetaInformationId",
                principalTable: "MetaInformations",
                principalColumn: "Id");
        }
    }
}

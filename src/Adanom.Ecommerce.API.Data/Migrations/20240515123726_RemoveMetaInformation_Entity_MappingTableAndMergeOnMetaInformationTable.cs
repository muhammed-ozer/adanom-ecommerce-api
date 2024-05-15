using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMetaInformation_Entity_MappingTableAndMergeOnMetaInformationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaInformation_Entity_Mappings");

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "MetaInformations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "EntityType",
                table: "MetaInformations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "MetaInformations");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "MetaInformations");

            migrationBuilder.CreateTable(
                name: "MetaInformation_Entity_Mappings",
                columns: table => new
                {
                    MetaInformationId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaInformation_Entity_Mappings", x => new { x.MetaInformationId, x.EntityId, x.EntityType });
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetaInformation_Entity_Mappings_MetaInformationId",
                table: "MetaInformation_Entity_Mappings",
                column: "MetaInformationId",
                unique: true);
        }
    }
}

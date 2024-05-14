using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adanom.Ecommerce.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateKeyConstraintsToMetaInformation_EntityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaInformation_Entity_Mappings",
                table: "MetaInformation_Entity_Mappings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MetaInformation_Entity_Mappings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaInformation_Entity_Mappings",
                table: "MetaInformation_Entity_Mappings",
                columns: new[] { "MetaInformationId", "EntityId", "EntityType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaInformation_Entity_Mappings",
                table: "MetaInformation_Entity_Mappings");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "MetaInformation_Entity_Mappings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaInformation_Entity_Mappings",
                table: "MetaInformation_Entity_Mappings",
                column: "Id");
        }
    }
}

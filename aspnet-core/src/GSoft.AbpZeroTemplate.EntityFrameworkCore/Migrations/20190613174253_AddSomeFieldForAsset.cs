using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class AddSomeFieldForAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "Manufacturers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Manufacturers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "AssetTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "AssetLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AssetLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "AssetTypes");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "AssetLines");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AssetLines");
        }
    }
}

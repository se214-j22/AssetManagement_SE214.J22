using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class AddSomeFiledForReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "DepreciationRate",
                table: "AssetTypes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "DepreciationMonths",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "DepreciationValue",
                table: "Assets",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FullDepreciationPrice",
                table: "Assets",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<long>(
                name: "PONumber",
                table: "Assets",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepreciationRate",
                table: "AssetTypes");

            migrationBuilder.DropColumn(
                name: "DepreciationMonths",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DepreciationValue",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FullDepreciationPrice",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PONumber",
                table: "Assets");
        }
    }
}

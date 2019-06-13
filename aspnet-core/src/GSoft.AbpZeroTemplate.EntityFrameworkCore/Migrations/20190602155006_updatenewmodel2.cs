using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class updatenewmodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusApproved",
                table: "AssetGroups");

            migrationBuilder.RenameColumn(
                name: "OfficialUseDate",
                table: "UseAssets",
                newName: "DateExport");

            migrationBuilder.AlterColumn<bool>(
                name: "StatusApproved",
                table: "UseAssets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "StatusApproved",
                table: "Transfers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "StatusApproved",
                table: "Revokes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "RepairUnit",
                table: "Repairs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ExportDate",
                table: "Repairs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "Repairs",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<bool>(
                name: "StatusApproved",
                table: "Repairs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "StatusApproved",
                table: "Liquidations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "StatusApproved",
                table: "Assets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusApproved",
                table: "Revokes");

            migrationBuilder.DropColumn(
                name: "StatusApproved",
                table: "Repairs");

            migrationBuilder.RenameColumn(
                name: "DateExport",
                table: "UseAssets",
                newName: "OfficialUseDate");

            migrationBuilder.AlterColumn<string>(
                name: "StatusApproved",
                table: "UseAssets",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "StatusApproved",
                table: "Transfers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "RepairUnit",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExportDate",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatusApproved",
                table: "Liquidations",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "StatusApproved",
                table: "Assets",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<string>(
                name: "StatusApproved",
                table: "AssetGroups",
                nullable: true);
        }
    }
}

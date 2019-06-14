using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class FixThuHoi_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongTh",
                table: "ThuHois");

            migrationBuilder.AlterColumn<string>(
                name: "MaTS",
                table: "ThuHois",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaTS",
                table: "ThuHois",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoLuongTh",
                table: "ThuHois",
                nullable: false,
                defaultValue: 0);
        }
    }
}

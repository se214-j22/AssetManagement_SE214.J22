
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class Fix_XuatTS_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaTaiSan",
                table: "XuatTaiSans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "TinhTrang",
                table: "ThongTinTaiSans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TinhTrang",
                table: "ThongTinTaiSans");

            migrationBuilder.AlterColumn<int>(
                name: "MaTaiSan",
                table: "XuatTaiSans",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

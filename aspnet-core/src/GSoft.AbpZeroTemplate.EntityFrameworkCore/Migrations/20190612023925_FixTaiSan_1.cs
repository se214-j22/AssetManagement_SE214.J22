using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class FixTaiSan_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "ThongTinTaiSans");

            migrationBuilder.DropColumn(
                name: "SoLuongTon",
                table: "ThongTinTaiSans");

            migrationBuilder.RenameColumn(
                name: "DSSoseri",
                table: "ThongTinTaiSans",
                newName: "Soseri");

            migrationBuilder.AlterColumn<long>(
                name: "NguyenGia",
                table: "ThongTinTaiSans",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "LoaiTS",
                table: "ThongTinTaiSans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTS",
                table: "ThongTinTaiSans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiTS",
                table: "ThongTinTaiSans");

            migrationBuilder.DropColumn(
                name: "MaTS",
                table: "ThongTinTaiSans");

            migrationBuilder.RenameColumn(
                name: "Soseri",
                table: "ThongTinTaiSans",
                newName: "DSSoseri");

            migrationBuilder.AlterColumn<int>(
                name: "NguyenGia",
                table: "ThongTinTaiSans",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "ThongTinTaiSans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLuongTon",
                table: "ThongTinTaiSans",
                nullable: false,
                defaultValue: 0);
        }
    }
}

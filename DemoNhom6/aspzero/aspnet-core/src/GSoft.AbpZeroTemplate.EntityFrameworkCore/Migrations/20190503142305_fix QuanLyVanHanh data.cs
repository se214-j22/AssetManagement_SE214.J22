using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class fixQuanLyVanHanhdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoKM",
                table: "QuanLyVanHanhs");

            migrationBuilder.AlterColumn<float>(
                name: "kmMoi",
                table: "QuanLyVanHanhs",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "kmCu",
                table: "QuanLyVanHanhs",
                nullable: true,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "kmMoi",
                table: "QuanLyVanHanhs",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "kmCu",
                table: "QuanLyVanHanhs",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoKM",
                table: "QuanLyVanHanhs",
                nullable: true);
        }
    }
}

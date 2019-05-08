using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class updateThongTinVanHanh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "kmCu",
                table: "QuanLyVanHanhs",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "kmMoi",
                table: "QuanLyVanHanhs",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kmCu",
                table: "QuanLyVanHanhs");

            migrationBuilder.DropColumn(
                name: "kmMoi",
                table: "QuanLyVanHanhs");
        }
    }
}

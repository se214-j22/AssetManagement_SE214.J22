using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class _5_6_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNhanVien",
                table: "ThuHois");

            migrationBuilder.DropColumn(
                name: "TenNhanVien",
                table: "ThuHois");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaNhanVien",
                table: "ThuHois",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TenNhanVien",
                table: "ThuHois",
                nullable: true);
        }
    }
}

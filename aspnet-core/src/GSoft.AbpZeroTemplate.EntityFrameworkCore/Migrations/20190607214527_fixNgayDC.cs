using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class fixNgayDC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NgayDC",
                table: "DieuChuyens",
                newName: "NgayDieuChuyen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NgayDieuChuyen",
                table: "DieuChuyens",
                newName: "NgayDC");
        }
    }
}

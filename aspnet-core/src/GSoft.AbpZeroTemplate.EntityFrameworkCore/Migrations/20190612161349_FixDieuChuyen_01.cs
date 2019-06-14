using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class FixDieuChuyen_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenNhanVienDC",
                table: "DieuChuyens",
                newName: "TenDonViNhan");

            migrationBuilder.RenameColumn(
                name: "TenDonVi",
                table: "DieuChuyens",
                newName: "TenDonViDC");

            migrationBuilder.RenameColumn(
                name: "MaNhanVienDC",
                table: "DieuChuyens",
                newName: "MaDVNhan");

            migrationBuilder.RenameColumn(
                name: "MaDonVi",
                table: "DieuChuyens",
                newName: "MaDVDC");

            migrationBuilder.AlterColumn<string>(
                name: "MaTaiSan",
                table: "DieuChuyens",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenDonViNhan",
                table: "DieuChuyens",
                newName: "TenNhanVienDC");

            migrationBuilder.RenameColumn(
                name: "TenDonViDC",
                table: "DieuChuyens",
                newName: "TenDonVi");

            migrationBuilder.RenameColumn(
                name: "MaDVNhan",
                table: "DieuChuyens",
                newName: "MaNhanVienDC");

            migrationBuilder.RenameColumn(
                name: "MaDVDC",
                table: "DieuChuyens",
                newName: "MaDonVi");

            migrationBuilder.AlterColumn<int>(
                name: "MaTaiSan",
                table: "DieuChuyens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

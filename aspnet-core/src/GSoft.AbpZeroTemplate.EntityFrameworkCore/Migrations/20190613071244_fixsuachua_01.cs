using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class fixsuachua_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenNhanVienpT",
                table: "SuaChuas",
                newName: "TenNhanVienPT");

            migrationBuilder.RenameColumn(
                name: "TenDonVi",
                table: "SuaChuas",
                newName: "TenDVSuaChua");

            migrationBuilder.RenameColumn(
                name: "MaDV",
                table: "SuaChuas",
                newName: "MaDVSuaChua");

            migrationBuilder.AlterColumn<string>(
                name: "MaTS",
                table: "SuaChuas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "ChiPhiduKien",
                table: "SuaChuas",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MaDVDeXuat",
                table: "SuaChuas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuKienSuaXong",
                table: "SuaChuas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySuaXong",
                table: "SuaChuas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayXuat",
                table: "SuaChuas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TenDVDeXuat",
                table: "SuaChuas",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ThayDoiCongNang",
                table: "SuaChuas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaDVDeXuat",
                table: "SuaChuas");

            migrationBuilder.DropColumn(
                name: "NgayDuKienSuaXong",
                table: "SuaChuas");

            migrationBuilder.DropColumn(
                name: "NgaySuaXong",
                table: "SuaChuas");

            migrationBuilder.DropColumn(
                name: "NgayXuat",
                table: "SuaChuas");

            migrationBuilder.DropColumn(
                name: "TenDVDeXuat",
                table: "SuaChuas");

            migrationBuilder.DropColumn(
                name: "ThayDoiCongNang",
                table: "SuaChuas");

            migrationBuilder.RenameColumn(
                name: "TenNhanVienPT",
                table: "SuaChuas",
                newName: "TenNhanVienpT");

            migrationBuilder.RenameColumn(
                name: "TenDVSuaChua",
                table: "SuaChuas",
                newName: "TenDonVi");

            migrationBuilder.RenameColumn(
                name: "MaDVSuaChua",
                table: "SuaChuas",
                newName: "MaDV");

            migrationBuilder.AlterColumn<int>(
                name: "MaTS",
                table: "SuaChuas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChiPhiduKien",
                table: "SuaChuas",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}

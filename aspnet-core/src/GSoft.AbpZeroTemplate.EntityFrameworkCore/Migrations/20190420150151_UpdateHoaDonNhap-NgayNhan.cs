using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class UpdateHoaDonNhapNgayNhan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhan",
                table: "HoaDonNhaps",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhan",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}

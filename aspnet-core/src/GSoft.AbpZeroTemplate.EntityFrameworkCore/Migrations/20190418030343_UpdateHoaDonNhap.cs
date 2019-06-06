using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class UpdateHoaDonNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Thue",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "NguyenGiaTaiSan",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "LePhi",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "GiaMuaThucTe",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "ChiPhiVanChuyen",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "ChiPhiSuaChua",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "ChiPhiNangCap",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "ChiPhiLapDatChayThu",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Thue",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "NguyenGiaTaiSan",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "LePhi",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "GiaMuaThucTe",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "ChiPhiVanChuyen",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "ChiPhiSuaChua",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "ChiPhiNangCap",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "ChiPhiLapDatChayThu",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}

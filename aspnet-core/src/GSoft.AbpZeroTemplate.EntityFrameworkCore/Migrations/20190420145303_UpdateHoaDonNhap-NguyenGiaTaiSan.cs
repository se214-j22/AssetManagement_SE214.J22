using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class UpdateHoaDonNhapNguyenGiaTaiSan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NguyenGiaTaiSan",
                table: "HoaDonNhaps",
                nullable: false,
                computedColumnSql: "[GiaMuaThucTe] + [ChiPhiVanChuyen] + [ChiPhiSuaChua] + [ChiPhiNangCap] + [ChiPhiLapDatChayThu] + [Thue] + [LePhi]",
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NguyenGiaTaiSan",
                table: "HoaDonNhaps",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComputedColumnSql: "[GiaMuaThucTe] + [ChiPhiVanChuyen] + [ChiPhiSuaChua] + [ChiPhiNangCap] + [ChiPhiLapDatChayThu] + [Thue] + [LePhi]");
        }
    }
}

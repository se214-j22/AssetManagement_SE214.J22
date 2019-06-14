using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class Customer_QuanLyCongTrinhXayDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers_QuanLyCongTrinhXayDung",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    maBDS = table.Column<string>(nullable: true),
                    TenCongTrinh = table.Column<string>(nullable: true),
                    DiaChiCongTrinh = table.Column<string>(nullable: true),
                    DienTichCongTrinh = table.Column<string>(nullable: true),
                    LoaiThucHienCongTrinh = table.Column<string>(nullable: true),
                    ChiPhiDuToanBanDau = table.Column<string>(nullable: true),
                    ChiPhiThucHien = table.Column<string>(nullable: true),
                    ChiPhiPhatSinh = table.Column<string>(nullable: true),
                    MoTaCongTrinh = table.Column<string>(nullable: true),
                    ThoiGianDuKienHoanThanh = table.Column<string>(nullable: true),
                    ThoiGianHoanThanh = table.Column<string>(nullable: true),
                    TienDoCongTrinh = table.Column<string>(nullable: true),
                    SoToTrinh = table.Column<string>(nullable: true),
                    ChiPhiDuKien = table.Column<string>(nullable: true),
                    ChiPhiDuocDuyet = table.Column<string>(nullable: true),
                    MaGoiThau = table.Column<string>(nullable: true),
                    TenHangMucGoiThau = table.Column<string>(nullable: true),
                    DonViTrungThau = table.Column<string>(nullable: true),
                    GiaTriChaoThau = table.Column<string>(nullable: true),
                    NgayNopHoSoThau = table.Column<string>(nullable: true),
                    ThoiGianThiCong = table.Column<string>(nullable: true),
                    ThongTinDonViThamGiaThau = table.Column<string>(nullable: true),
                    TongGiaTriHopDong = table.Column<string>(nullable: true),
                    GiaTriDaThanhToanCuaHopDong = table.Column<string>(nullable: true),
                    TienDoThiCong = table.Column<string>(nullable: true),
                    ThoiGianThanhToan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers_QuanLyCongTrinhXayDung", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers_QuanLyCongTrinhXayDung");
        }
    }
}

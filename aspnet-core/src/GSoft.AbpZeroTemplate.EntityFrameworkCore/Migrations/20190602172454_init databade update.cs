using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class initdatabadeupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    model = table.Column<string>(nullable: true),
                    tenModel = table.Column<string>(nullable: true),
                    loaiXe = table.Column<string>(nullable: true),
                    hangSanXuat = table.Column<string>(nullable: true),
                    dinhMucNhienLieu = table.Column<float>(nullable: true),
                    ghiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    maCongTyBaoHiem = table.Column<string>(nullable: true),
                    tenCongTyBaoHiem = table.Column<string>(nullable: true),
                    diaChi = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    maSoThue = table.Column<string>(nullable: true),
                    soDienThoai = table.Column<string>(nullable: true),
                    nguoiLienHe = table.Column<string>(nullable: true),
                    hoatDong = table.Column<bool>(nullable: false),
                    ghiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhiDuongBos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    soXe = table.Column<string>(nullable: true),
                    ngayCapNhat = table.Column<DateTime>(nullable: true),
                    ngayDongPhi = table.Column<DateTime>(nullable: true),
                    ngayHetHanDongPhi = table.Column<DateTime>(nullable: true),
                    thoiGianSuDung = table.Column<int>(nullable: true),
                    soTienThanhToan = table.Column<double>(nullable: true),
                    congTyThuPhi = table.Column<string>(nullable: true),
                    loaiPhi = table.Column<string>(nullable: true),
                    ghiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhiDuongBos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    maTaiSan = table.Column<string>(nullable: true),
                    tenTaiSan = table.Column<string>(nullable: true),
                    nhomTaiSan = table.Column<string>(nullable: true),
                    loaiTaiSan = table.Column<string>(nullable: true),
                    thongTinMoTa = table.Column<string>(nullable: true),
                    nguyenGiaTaiSan = table.Column<long>(nullable: true),
                    donViSuDung = table.Column<string>(nullable: true),
                    tinhTrangTaiSan = table.Column<string>(nullable: true),
                    nguoiSuDung = table.Column<string>(nullable: true),
                    tinhTrangKhauHao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiSans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThietBiKemTheos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    soXe = table.Column<string>(nullable: true),
                    thietBiKemTheo = table.Column<string>(nullable: true),
                    soLuong = table.Column<int>(nullable: true),
                    dienGiai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThietBiKemTheos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinBaoHiem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    soXe = table.Column<string>(nullable: true),
                    ngayMuaBaoHiem = table.Column<DateTime>(nullable: true),
                    ngayHetHanBaoHiem = table.Column<DateTime>(nullable: true),
                    thoiHanBaoHiem = table.Column<int>(nullable: true),
                    congTyBaoHiem = table.Column<string>(nullable: true),
                    loaiBaoHiem = table.Column<string>(nullable: true),
                    soTienThanhToan = table.Column<double>(nullable: true),
                    trangThaiDuyet = table.Column<string>(nullable: true),
                    ghiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinBaoHiem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinDangKiems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    soXe = table.Column<string>(nullable: true),
                    ngayDangKiem = table.Column<DateTime>(nullable: false),
                    ngayHetHanDangKiem = table.Column<DateTime>(nullable: false),
                    thoiHanDangKiem = table.Column<int>(nullable: true),
                    coQuanDangKiem = table.Column<string>(nullable: true),
                    trangThaiDuyet = table.Column<string>(nullable: true),
                    ghiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinDangKiems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinSuaChuas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    soXe = table.Column<string>(nullable: true),
                    ngaySuaChua = table.Column<DateTime>(nullable: true),
                    ngayDuKienSuaXong = table.Column<DateTime>(nullable: true),
                    chiPhiSuaChua = table.Column<double>(nullable: true),
                    noiDungSuaChuaThucTe = table.Column<string>(nullable: true),
                    trangThaiDuyet = table.Column<string>(nullable: true),
                    ghiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinSuaChuas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinXes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    soXe = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true),
                    nuocSanXuat = table.Column<string>(nullable: true),
                    loaiNhienLieu = table.Column<string>(nullable: true),
                    namSanXuat = table.Column<int>(nullable: true),
                    mauXe = table.Column<string>(nullable: true),
                    maTaiSan = table.Column<string>(nullable: true),
                    mucDichSuDung = table.Column<string>(nullable: true),
                    ngayDangKiBanDau = table.Column<DateTime>(nullable: true),
                    soMay = table.Column<string>(nullable: true),
                    soSuon = table.Column<string>(nullable: true),
                    coLopSuDung = table.Column<string>(nullable: true),
                    kieuDongCo = table.Column<string>(nullable: true),
                    loaiHopSo = table.Column<string>(nullable: true),
                    theTichDongCo = table.Column<float>(nullable: true),
                    chieuDai = table.Column<float>(nullable: true),
                    chieuCao = table.Column<float>(nullable: true),
                    chieuNgang = table.Column<float>(nullable: true),
                    trangThaiDuyet = table.Column<string>(nullable: true),
                    donViSuDung = table.Column<string>(nullable: true),
                    tenChuPhuongTien = table.Column<string>(nullable: true),
                    organizationUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinXes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

            migrationBuilder.DropTable(
                name: "PhiDuongBos");

            migrationBuilder.DropTable(
                name: "TaiSans");

            migrationBuilder.DropTable(
                name: "ThietBiKemTheos");

            migrationBuilder.DropTable(
                name: "ThongTinBaoHiem");

            migrationBuilder.DropTable(
                name: "ThongTinDangKiems");

            migrationBuilder.DropTable(
                name: "ThongTinSuaChuas");

            migrationBuilder.DropTable(
                name: "ThongTinXes");
        }
    }
}

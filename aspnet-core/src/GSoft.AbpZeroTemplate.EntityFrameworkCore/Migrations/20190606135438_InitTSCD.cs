using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class InitTSCD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelDemos");

            migrationBuilder.CreateTable(
                name: "DonViCungCapTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenDonViCungCap = table.Column<string>(nullable: true),
                    DiaChiDonViCungCap = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViCungCapTaiSans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenNhomTaiSan = table.Column<string>(nullable: true),
                    TiLeHaoMon = table.Column<float>(nullable: false),
                    ThoiHanSuDung = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiTaiSans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenPhong = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speedsters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    SpeedsterId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speedsters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonNhaps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    donViCungCapTaiSanId = table.Column<int>(nullable: true),
                    NgayNhan = table.Column<DateTime>(type: "date", nullable: false),
                    SoHoaDon = table.Column<string>(nullable: true),
                    GiaMuaThucTe = table.Column<decimal>(nullable: false),
                    ChiPhiVanChuyen = table.Column<decimal>(nullable: false),
                    ChiPhiSuaChua = table.Column<decimal>(nullable: false),
                    ChiPhiNangCap = table.Column<decimal>(nullable: false),
                    ChiPhiLapDatChayThu = table.Column<decimal>(nullable: false),
                    Thue = table.Column<decimal>(nullable: false),
                    LePhi = table.Column<decimal>(nullable: false),
                    NguyenGiaTaiSan = table.Column<decimal>(nullable: false, computedColumnSql: "[GiaMuaThucTe] + [ChiPhiVanChuyen] + [ChiPhiSuaChua] + [ChiPhiNangCap] + [ChiPhiLapDatChayThu] + [Thue] + [LePhi]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonNhaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonNhaps_DonViCungCapTaiSans_donViCungCapTaiSanId",
                        column: x => x.donViCungCapTaiSanId,
                        principalTable: "DonViCungCapTaiSans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BangYeuCauCungCapTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    PhongBanId = table.Column<int>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    NgayYeuCau = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangYeuCauCungCapTaiSans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BangYeuCauCungCapTaiSans_PhongBans_PhongBanId",
                        column: x => x.PhongBanId,
                        principalTable: "PhongBans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangYeuCauCungCapTaiSans_PhongBanId",
                table: "BangYeuCauCungCapTaiSans",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhaps_donViCungCapTaiSanId",
                table: "HoaDonNhaps",
                column: "donViCungCapTaiSanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangYeuCauCungCapTaiSans");

            migrationBuilder.DropTable(
                name: "HoaDonNhaps");

            migrationBuilder.DropTable(
                name: "LoaiTaiSans");

            migrationBuilder.DropTable(
                name: "Speedsters");

            migrationBuilder.DropTable(
                name: "PhongBans");

            migrationBuilder.DropTable(
                name: "DonViCungCapTaiSans");

            migrationBuilder.CreateTable(
                name: "ModelDemos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelDemos", x => x.Id);
                });
        }
    }
}

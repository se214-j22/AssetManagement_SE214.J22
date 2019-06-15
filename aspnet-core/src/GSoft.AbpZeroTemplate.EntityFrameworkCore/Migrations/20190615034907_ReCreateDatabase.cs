using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class ReCreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangYeuCauCungCapTaiSans");

            migrationBuilder.DropTable(
                name: "BienBanBanGiaoTaiSans");

            migrationBuilder.DropTable(
                name: "BienBanThanhLys");

            migrationBuilder.DropTable(
                name: "HoaDonNhaps");

            migrationBuilder.DropTable(
                name: "LoaiTaiSans");

            migrationBuilder.DropTable(
                name: "PhieuBaoDuongs");

            migrationBuilder.DropTable(
                name: "Speedsters");

            migrationBuilder.DropTable(
                name: "TaiSanCoDinhs");

            migrationBuilder.DropTable(
                name: "PhongBans");

            migrationBuilder.DropTable(
                name: "DonViCungCapTaiSans");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "Customers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AssetGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetGrouptId = table.Column<string>(nullable: true),
                    AssetGroupName = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetGroupParentId = table.Column<string>(nullable: true),
                    MonthOfDepreciation = table.Column<int>(nullable: false),
                    DepreciationRates = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    AssetName = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetGrouptId = table.Column<string>(nullable: true),
                    DateAdded = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
                    MonthOfDepreciation = table.Column<int>(nullable: false),
                    OriginalPrice = table.Column<float>(nullable: false),
                    DepreciationValue = table.Column<float>(nullable: false),
                    WarrantyExpiryDate = table.Column<string>(nullable: true),
                    ProviderId = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Liquidations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetID = table.Column<string>(nullable: true),
                    LiquidationDate = table.Column<string>(nullable: true),
                    PurchaseUnit = table.Column<int>(nullable: false),
                    AssetStatus = table.Column<string>(nullable: true),
                    LiquidationForm = table.Column<int>(nullable: false),
                    LiquidationPrice = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StatusApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    ExportDate = table.Column<string>(nullable: true),
                    ExpectedDateRepaired = table.Column<string>(nullable: true),
                    ExpectedRepairUnit = table.Column<string>(nullable: true),
                    Proposer = table.Column<string>(nullable: true),
                    StaffInCharge = table.Column<string>(nullable: true),
                    ExpectedCost = table.Column<float>(nullable: false),
                    ExpectedContent = table.Column<string>(nullable: true),
                    ExpectedNote = table.Column<string>(nullable: true),
                    DateRepaired = table.Column<string>(nullable: true),
                    RepairUnit = table.Column<int>(nullable: true),
                    Cost = table.Column<float>(nullable: true),
                    IsChangeFunction = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    StatusApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Revokes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    RevokeDate = table.Column<string>(nullable: true),
                    AssetId = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    RevokeContent = table.Column<string>(nullable: true),
                    AssetStatus = table.Column<string>(nullable: true),
                    CurrentLocationOfAssets = table.Column<string>(nullable: true),
                    StatusApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revokes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    TransferDate = table.Column<string>(nullable: true),
                    ReceivingUnit = table.Column<string>(nullable: true),
                    Receiver = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StatusApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseAssets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    UnitsUsedId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DateExport = table.Column<string>(nullable: true),
                    StatusApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseAssets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetGroups");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Liquidations");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Revokes");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "UseAssets");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "BienBanBanGiaoTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NgayNhan = table.Column<DateTime>(nullable: false),
                    PhongBanId = table.Column<int>(nullable: false),
                    TaiSanCoDinhId = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienBanBanGiaoTaiSans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BienBanThanhLys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChiPhiThanhLy = table.Column<decimal>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    LoiNhuan = table.Column<decimal>(nullable: false),
                    NgayThanhLy = table.Column<DateTime>(nullable: false),
                    TaiSanCoDinhId = table.Column<int>(nullable: false),
                    TenDonViThanhLy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienBanThanhLys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonViCungCapTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DiaChiDonViCungCap = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenDonViCungCap = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
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
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenNhomTaiSan = table.Column<string>(nullable: true),
                    ThoiHanSuDung = table.Column<float>(nullable: false),
                    TiLeHaoMon = table.Column<float>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiTaiSans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhieuBaoDuongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChiPhi = table.Column<decimal>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    SoHoaDon = table.Column<string>(nullable: true),
                    TaiSanCoDinhId = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuBaoDuongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenPhong = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
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
                    Address = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SpeedsterId = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speedsters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiSanCoDinhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    GiaTriTaiSan = table.Column<decimal>(nullable: false),
                    HaoMonTaiSan = table.Column<decimal>(nullable: false),
                    HoaDonNhapId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    LoaiTaiSanId = table.Column<int>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    TenTaiSan = table.Column<string>(nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiSanCoDinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonNhaps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChiPhiLapDatChayThu = table.Column<decimal>(nullable: false),
                    ChiPhiNangCap = table.Column<decimal>(nullable: false),
                    ChiPhiSuaChua = table.Column<decimal>(nullable: false),
                    ChiPhiVanChuyen = table.Column<decimal>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    GiaMuaThucTe = table.Column<decimal>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    LePhi = table.Column<decimal>(nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "date", nullable: false),
                    NguyenGiaTaiSan = table.Column<decimal>(nullable: false, computedColumnSql: "[GiaMuaThucTe] + [ChiPhiVanChuyen] + [ChiPhiSuaChua] + [ChiPhiNangCap] + [ChiPhiLapDatChayThu] + [Thue] + [LePhi]"),
                    SoHoaDon = table.Column<string>(nullable: true),
                    Thue = table.Column<decimal>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    donViCungCapTaiSanId = table.Column<int>(nullable: true)
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
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    NgayYeuCau = table.Column<DateTime>(type: "date", nullable: false),
                    PhongBanId = table.Column<int>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
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
    }
}

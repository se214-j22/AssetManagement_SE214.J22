using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class KhuVucs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenDVDuocDieuChuyen",
                table: "DieuChuyens");

            migrationBuilder.AddColumn<int>(
                name: "MaDonVi",
                table: "ThuHois",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GiaTriKhauHao",
                table: "TaiSans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaLo",
                table: "TaiSans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayNhap",
                table: "TaiSans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SoSeri",
                table: "TaiSans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThongTinMoTa",
                table: "TaiSans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaDonViDieuChuyen",
                table: "DieuChuyens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaDonViDuocDieuChuyen",
                table: "DieuChuyens",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChiNhanhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenChiNhanh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiNhanhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonVis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaKhuVuc = table.Column<int>(nullable: true),
                    MaChiNhanh = table.Column<int>(nullable: true),
                    TenDonVi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonVis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhuVucs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenKhuVuc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuVucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    SoLuongTS = table.Column<string>(nullable: true),
                    TongGiaTri = table.Column<string>(nullable: true),
                    NgayNhap = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoTaiSans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiNhanhs");

            migrationBuilder.DropTable(
                name: "DonVis");

            migrationBuilder.DropTable(
                name: "KhuVucs");

            migrationBuilder.DropTable(
                name: "LoTaiSans");

            migrationBuilder.DropColumn(
                name: "MaDonVi",
                table: "ThuHois");

            migrationBuilder.DropColumn(
                name: "GiaTriKhauHao",
                table: "TaiSans");

            migrationBuilder.DropColumn(
                name: "MaLo",
                table: "TaiSans");

            migrationBuilder.DropColumn(
                name: "NgayNhap",
                table: "TaiSans");

            migrationBuilder.DropColumn(
                name: "SoSeri",
                table: "TaiSans");

            migrationBuilder.DropColumn(
                name: "ThongTinMoTa",
                table: "TaiSans");

            migrationBuilder.DropColumn(
                name: "MaDonViDieuChuyen",
                table: "DieuChuyens");

            migrationBuilder.DropColumn(
                name: "MaDonViDuocDieuChuyen",
                table: "DieuChuyens");

            migrationBuilder.AddColumn<string>(
                name: "TenDVDuocDieuChuyen",
                table: "DieuChuyens",
                nullable: true);
        }
    }
}

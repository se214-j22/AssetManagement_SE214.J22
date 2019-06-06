using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class AddHoaDonNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    NgayNhan = table.Column<DateTime>(nullable: false),
                    SoHoaDon = table.Column<string>(nullable: true),
                    GiaMuaThucTe = table.Column<float>(nullable: false),
                    ChiPhiVanChuyen = table.Column<float>(nullable: false),
                    ChiPhiSuaChua = table.Column<float>(nullable: false),
                    ChiPhiNangCap = table.Column<float>(nullable: false),
                    ChiPhiLapDatChayThu = table.Column<float>(nullable: false),
                    Thue = table.Column<float>(nullable: false),
                    LePhi = table.Column<float>(nullable: false),
                    NguyenGiaTaiSan = table.Column<float>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhaps_donViCungCapTaiSanId",
                table: "HoaDonNhaps",
                column: "donViCungCapTaiSanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoaDonNhaps");
        }
    }
}

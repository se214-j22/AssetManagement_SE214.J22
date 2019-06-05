using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class DieuChuyens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DieuChuyens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaTaiSan = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    TenTaiSan = table.Column<string>(nullable: true),
                    MaNhanVienDC = table.Column<int>(nullable: false),
                    TenNhanVienDC = table.Column<string>(nullable: true),
                    MaDonVi = table.Column<int>(nullable: false),
                    TenDonVi = table.Column<string>(nullable: true),
                    MaNhanVienNhan = table.Column<int>(nullable: false),
                    TenNhanVienNhan = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DieuChuyens", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DieuChuyens");
        }
    }
}

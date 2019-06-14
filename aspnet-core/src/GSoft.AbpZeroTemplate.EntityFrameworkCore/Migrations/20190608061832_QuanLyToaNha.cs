using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class QuanLyToaNha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers_QuanLyToaNha",
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
                    ThongTinKhachThue = table.Column<string>(nullable: true),
                    PhiDichVu = table.Column<string>(nullable: true),
                    NgayVao = table.Column<string>(nullable: true),
                    NgayRa = table.Column<string>(nullable: true),
                    GiaHan = table.Column<string>(nullable: true),
                    KhuVucThue = table.Column<string>(nullable: true),
                    LichSuThueSanPham = table.Column<string>(nullable: true),
                    DanhSachSanPham = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers_QuanLyToaNha", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers_QuanLyToaNha");
        }
    }
}

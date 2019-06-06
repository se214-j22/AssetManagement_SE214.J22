using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class AddTaiSanCoDinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaiSanCoDinhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    LoaiTaiSanId = table.Column<int>(nullable: false),
                    HoaDonNhapId = table.Column<int>(nullable: false),
                    TenTaiSan = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    TinhTrang = table.Column<bool>(nullable: false),
                    GiaTriTaiSan = table.Column<decimal>(nullable: false),
                    HaoMonTaiSan = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiSanCoDinhs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiSanCoDinhs");
        }
    }
}

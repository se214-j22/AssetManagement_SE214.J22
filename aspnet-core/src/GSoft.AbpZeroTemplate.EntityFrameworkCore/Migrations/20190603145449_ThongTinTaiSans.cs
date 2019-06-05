using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class ThongTinTaiSans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThongTinTaiSans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    TenTs = table.Column<string>(nullable: true),
                    ThongTinMoTa = table.Column<string>(nullable: true),
                    NgayNhap = table.Column<DateTime>(nullable: false),
                    MaNhomTS = table.Column<int>(nullable: false),
                    MaLo = table.Column<int>(nullable: true),
                    NguyenGia = table.Column<int>(nullable: false),
                    DSSoseri = table.Column<string>(nullable: true),
                    GiaTriKhauHao = table.Column<int>(nullable: false),
                    SoThangKhauHao = table.Column<int>(nullable: false),
                    TyLeKhauHao = table.Column<float>(nullable: false),
                    SoThangBaoHanh = table.Column<int>(nullable: false),
                    TinhTrangKhauHao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinTaiSans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongTinTaiSans");
        }
    }
}

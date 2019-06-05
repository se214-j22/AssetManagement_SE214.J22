using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class ThuHois : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenKhuVuc",
                table: "KhuVucs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThuHois",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaNhanVien = table.Column<int>(nullable: false),
                    TenNhanVien = table.Column<string>(nullable: true),
                    MaDV = table.Column<int>(nullable: false),
                    TenDonVi = table.Column<string>(nullable: true),
                    MaTS = table.Column<int>(nullable: false),
                    TenTaiSan = table.Column<string>(nullable: true),
                    SoLuongTh = table.Column<int>(nullable: false),
                    NgayThuHoi = table.Column<DateTime>(nullable: false),
                    LyDo = table.Column<string>(nullable: true),
                    TrangThaiDuyet = table.Column<bool>(nullable: false),
                    NoiDungTh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuHois", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThuHois");

            migrationBuilder.DropColumn(
                name: "TenKhuVuc",
                table: "KhuVucs");
        }
    }
}

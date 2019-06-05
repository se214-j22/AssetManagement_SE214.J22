using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class _5_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDC",
                table: "DieuChuyens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "ChiNhanhs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaDonVi",
                table: "ChiNhanhs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CTDonVis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaDV = table.Column<int>(nullable: false),
                    MaTS = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    TenDonVi = table.Column<string>(nullable: true),
                    TenTaiSan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTDonVis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTDonVis");

            migrationBuilder.DropColumn(
                name: "NgayDC",
                table: "DieuChuyens");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "ChiNhanhs");

            migrationBuilder.DropColumn(
                name: "MaDonVi",
                table: "ChiNhanhs");
        }
    }
}

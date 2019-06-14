using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class ThanhLy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayTest",
                table: "DonVis");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "DonVis",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThanhLies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaTS = table.Column<string>(nullable: true),
                    TenTS = table.Column<string>(nullable: true),
                    MaDonViMua = table.Column<int>(nullable: false),
                    DonViMua = table.Column<string>(nullable: true),
                    HinhThucThanhLy = table.Column<string>(nullable: true),
                    GiaTienThanhLy = table.Column<long>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhLies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThanhLies");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "DonVis");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTest",
                table: "DonVis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

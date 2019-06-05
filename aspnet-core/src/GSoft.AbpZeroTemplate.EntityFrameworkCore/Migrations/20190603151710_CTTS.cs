using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class CTTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaTriKhauHao",
                table: "TS");

            migrationBuilder.CreateTable(
                name: "CTTS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaTS = table.Column<int>(nullable: false),
                    MaLo = table.Column<int>(nullable: false),
                    SoSeri = table.Column<string>(nullable: true),
                    MaXuatTS = table.Column<int>(nullable: false),
                    MADC = table.Column<int>(nullable: false),
                    MATH = table.Column<int>(nullable: false),
                    MaSC = table.Column<int>(nullable: false),
                    MaTL = table.Column<int>(nullable: false),
                    MaDV = table.Column<int>(nullable: false),
                    TenDV = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTTS", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTTS");

            migrationBuilder.AddColumn<int>(
                name: "GiaTriKhauHao",
                table: "TS",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class SuaChuas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuaChuas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaNhanVienPT = table.Column<int>(nullable: false),
                    TenNhanVienpT = table.Column<string>(nullable: true),
                    MaNhanVienDX = table.Column<int>(nullable: false),
                    TenNhanVienDX = table.Column<string>(nullable: true),
                    MaDV = table.Column<int>(nullable: false),
                    TenDonVi = table.Column<string>(nullable: true),
                    MaTS = table.Column<int>(nullable: false),
                    TenTaiSan = table.Column<string>(nullable: true),
                    ChiPhiduKien = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    NoiDungSuaChua = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuaChuas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuaChuas");
        }
    }
}

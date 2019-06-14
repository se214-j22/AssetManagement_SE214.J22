using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class Customer_SuaChua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Customers_SuaChua",
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
                    ngayXuat = table.Column<string>(nullable: true),
                    ngaySuaXong = table.Column<string>(nullable: true),
                    tenBDS = table.Column<string>(nullable: true),
                    nguoiDeXuat = table.Column<string>(nullable: true),
                    nvPhuTrach = table.Column<string>(nullable: true),
                    trangThaiDuyet = table.Column<string>(nullable: true)
                   
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers_SuaChua", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
               name: "Customers_SuaChua");

        }
    }
}

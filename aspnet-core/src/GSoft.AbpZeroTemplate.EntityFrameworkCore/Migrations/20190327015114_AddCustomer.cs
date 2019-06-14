﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class AddCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    tenBDS = table.Column<string>(nullable: true),
                    loaiBDS = table.Column<string>(nullable: true),
                    nhomBDS = table.Column<string>(nullable: true),
                    maBDS = table.Column<string>(nullable: true),

                    hienTrang = table.Column<string>(nullable: true),
                    dienTich = table.Column<string>(nullable: true),
                    dai = table.Column<string>(nullable: true),
                    rong = table.Column<string>(nullable: true),
                    thoiHanSD = table.Column<string>(nullable: true),
                    tinhTrangSD = table.Column<string>(nullable: true),
                    ketCauNha = table.Column<string>(nullable: true),
                    ranhGioi = table.Column<string>(nullable: true),
                    tinhTrangPhapLy = table.Column<string>(nullable: true)
                    

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

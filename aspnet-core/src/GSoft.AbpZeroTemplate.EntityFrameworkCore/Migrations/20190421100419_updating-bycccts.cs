using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class updatingbycccts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanYeuCauCungCapTaiSans_PhongBans_PhongBanId",
                table: "BanYeuCauCungCapTaiSans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BanYeuCauCungCapTaiSans",
                table: "BanYeuCauCungCapTaiSans");

            migrationBuilder.RenameTable(
                name: "BanYeuCauCungCapTaiSans",
                newName: "BangYeuCauCungCapTaiSans");

            migrationBuilder.RenameIndex(
                name: "IX_BanYeuCauCungCapTaiSans_PhongBanId",
                table: "BangYeuCauCungCapTaiSans",
                newName: "IX_BangYeuCauCungCapTaiSans_PhongBanId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayYeuCau",
                table: "BangYeuCauCungCapTaiSans",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BangYeuCauCungCapTaiSans",
                table: "BangYeuCauCungCapTaiSans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BangYeuCauCungCapTaiSans_PhongBans_PhongBanId",
                table: "BangYeuCauCungCapTaiSans",
                column: "PhongBanId",
                principalTable: "PhongBans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BangYeuCauCungCapTaiSans_PhongBans_PhongBanId",
                table: "BangYeuCauCungCapTaiSans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BangYeuCauCungCapTaiSans",
                table: "BangYeuCauCungCapTaiSans");

            migrationBuilder.RenameTable(
                name: "BangYeuCauCungCapTaiSans",
                newName: "BanYeuCauCungCapTaiSans");

            migrationBuilder.RenameIndex(
                name: "IX_BangYeuCauCungCapTaiSans_PhongBanId",
                table: "BanYeuCauCungCapTaiSans",
                newName: "IX_BanYeuCauCungCapTaiSans_PhongBanId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayYeuCau",
                table: "BanYeuCauCungCapTaiSans",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BanYeuCauCungCapTaiSans",
                table: "BanYeuCauCungCapTaiSans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BanYeuCauCungCapTaiSans_PhongBans_PhongBanId",
                table: "BanYeuCauCungCapTaiSans",
                column: "PhongBanId",
                principalTable: "PhongBans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

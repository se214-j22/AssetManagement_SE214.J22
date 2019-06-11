using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class fixbid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidProfiles_Projects_ProjectId",
                table: "BidProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BidProfiles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartReceivedDate",
                table: "BidProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "BidProfiles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndReceivedDate",
                table: "BidProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "BidProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<float>(
                name: "CautionMoney",
                table: "BidProfiles",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "BidProfiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinStatus",
                table: "BidProfiles",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BidProfiles_Projects_ProjectId",
                table: "BidProfiles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidProfiles_Projects_ProjectId",
                table: "BidProfiles");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "BidProfiles");

            migrationBuilder.DropColumn(
                name: "WinStatus",
                table: "BidProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BidProfiles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartReceivedDate",
                table: "BidProfiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "BidProfiles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndReceivedDate",
                table: "BidProfiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "BidProfiles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "CautionMoney",
                table: "BidProfiles",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BidProfiles_Projects_ProjectId",
                table: "BidProfiles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

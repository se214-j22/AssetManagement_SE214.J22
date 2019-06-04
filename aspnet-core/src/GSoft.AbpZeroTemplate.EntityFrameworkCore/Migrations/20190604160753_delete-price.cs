using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class deleteprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AlterColumn<float>(
                name: "UnitPrice",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CautionMoney",
                table: "BidProfiles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndReceivedDate",
                table: "BidProfiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartReceivedDate",
                table: "BidProfiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CautionMoney",
                table: "BidProfiles");

            migrationBuilder.DropColumn(
                name: "EndReceivedDate",
                table: "BidProfiles");

            migrationBuilder.DropColumn(
                name: "StartReceivedDate",
                table: "BidProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UnitPrice",
                table: "Products",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Products",
                nullable: false,
                defaultValue: 0f);
        }
    }
}

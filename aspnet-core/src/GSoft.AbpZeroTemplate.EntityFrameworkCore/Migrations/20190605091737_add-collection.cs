using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addcollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidUnit_BidProfiles_BidProfileId",
                table: "BidUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_BidUnit_BidProfiles_BidProfileId",
                table: "BidUnit",
                column: "BidProfileId",
                principalTable: "BidProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidUnit_BidProfiles_BidProfileId",
                table: "BidUnit");

            migrationBuilder.AddForeignKey(
                name: "FK_BidUnit_BidProfiles_BidProfileId",
                table: "BidUnit",
                column: "BidProfileId",
                principalTable: "BidProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

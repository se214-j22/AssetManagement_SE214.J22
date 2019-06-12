using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "BidProfiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidProfiles_OrganizationUnitId",
                table: "BidProfiles",
                column: "OrganizationUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_BidProfiles_AbpOrganizationUnits_OrganizationUnitId",
                table: "BidProfiles",
                column: "OrganizationUnitId",
                principalTable: "AbpOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidProfiles_AbpOrganizationUnits_OrganizationUnitId",
                table: "BidProfiles");

            migrationBuilder.DropIndex(
                name: "IX_BidProfiles_OrganizationUnitId",
                table: "BidProfiles");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "BidProfiles");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addsubplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubPlans_Products_ProductId",
                table: "SubPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_SubPlans_Products_ProductId",
                table: "SubPlans",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubPlans_Products_ProductId",
                table: "SubPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_SubPlans_Products_ProductId",
                table: "SubPlans",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

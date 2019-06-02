using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addinfousers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "SubPlans");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SubPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubPlans_ProductId",
                table: "SubPlans",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubPlans_Products_ProductId",
                table: "SubPlans",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubPlans_Products_ProductId",
                table: "SubPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubPlans_ProductId",
                table: "SubPlans");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SubPlans");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "SubPlans",
                nullable: true);
        }
    }
}

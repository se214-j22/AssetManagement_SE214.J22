using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class updatemodelliquidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "ResidualValue",
                table: "Liquidations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "OriginalPrice",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ResidualValue",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0f);
        }
    }
}

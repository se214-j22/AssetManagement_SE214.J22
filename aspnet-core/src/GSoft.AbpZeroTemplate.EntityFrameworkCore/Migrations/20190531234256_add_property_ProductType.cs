using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class add_property_ProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInCludeSupplier",
                table: "ProductTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "IsInCludeSupplier",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductTypes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class FixTableSupplierType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInCludeSupplier",
                table: "SupplierTypes");

            migrationBuilder.DropColumn(
                name: "IsInCludeSupplier",
                table: "ProductTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInCludeSupplier",
                table: "SupplierTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInCludeSupplier",
                table: "ProductTypes",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SupplierTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "SupplierTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SupplierTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "SupplierTypes");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "SupplierTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SupplierTypes");
        }
    }
}

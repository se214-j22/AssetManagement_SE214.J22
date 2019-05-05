using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Biddings",
                table: "Biddings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Biddings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Biddings",
                table: "Biddings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Biddings_ProductId",
                table: "Biddings",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Biddings",
                table: "Biddings");

            migrationBuilder.DropIndex(
                name: "IX_Biddings_ProductId",
                table: "Biddings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Biddings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Biddings",
                table: "Biddings",
                columns: new[] { "ProductId", "SupplierId" });
        }
    }
}

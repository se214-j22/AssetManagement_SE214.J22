using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class kkk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Departments_DepartmentId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_DepartmentId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Plans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Plans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_DepartmentId",
                table: "Plans",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Departments_DepartmentId",
                table: "Plans",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

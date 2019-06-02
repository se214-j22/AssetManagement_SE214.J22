using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Plans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubPlanId",
                table: "Plans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_DepartmentId",
                table: "Plans",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_SubPlanId",
                table: "Plans",
                column: "SubPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Departments_DepartmentId",
                table: "Plans",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_SubPlans_SubPlanId",
                table: "Plans",
                column: "SubPlanId",
                principalTable: "SubPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Departments_DepartmentId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_SubPlans_SubPlanId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_DepartmentId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_SubPlanId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "SubPlanId",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "Plans",
                nullable: true);
        }
    }
}

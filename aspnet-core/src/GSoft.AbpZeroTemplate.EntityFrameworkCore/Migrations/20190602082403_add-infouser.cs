using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addinfouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_SubPlans_SubPlanId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_SubPlanId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "SubPlanId",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "SubPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubPlans_PlanId",
                table: "SubPlans",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubPlans_Plans_PlanId",
                table: "SubPlans",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubPlans_Plans_PlanId",
                table: "SubPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubPlans_PlanId",
                table: "SubPlans");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "SubPlans");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "AbpUsers");

            migrationBuilder.AddColumn<int>(
                name: "SubPlanId",
                table: "Plans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_SubPlanId",
                table: "Plans",
                column: "SubPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_SubPlans_SubPlanId",
                table: "Plans",
                column: "SubPlanId",
                principalTable: "SubPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

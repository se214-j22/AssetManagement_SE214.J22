using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PesidualQuantity",
                table: "SubPlans",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SubPlans",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SubPlans");

            migrationBuilder.AlterColumn<float>(
                name: "PesidualQuantity",
                table: "SubPlans",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

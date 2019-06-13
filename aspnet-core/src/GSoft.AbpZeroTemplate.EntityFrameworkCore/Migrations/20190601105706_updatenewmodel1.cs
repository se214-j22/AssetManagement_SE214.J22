using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class updatenewmodel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiquidationDetails");

            migrationBuilder.DropColumn(
                name: "LiquidationStatus",
                table: "Liquidations");

            migrationBuilder.RenameColumn(
                name: "TotalAsset",
                table: "Liquidations",
                newName: "LiquidationPrice");

            migrationBuilder.RenameColumn(
                name: "LiquidationType",
                table: "Liquidations",
                newName: "LiquidationForm");

            migrationBuilder.RenameColumn(
                name: "LiquidationId",
                table: "Liquidations",
                newName: "AssetStatus");

            migrationBuilder.RenameColumn(
                name: "AmountOfLiquidation",
                table: "Liquidations",
                newName: "ResidualValue");

            migrationBuilder.AddColumn<string>(
                name: "AssetID",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OriginalPrice",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetID",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Liquidations");

            migrationBuilder.RenameColumn(
                name: "ResidualValue",
                table: "Liquidations",
                newName: "AmountOfLiquidation");

            migrationBuilder.RenameColumn(
                name: "LiquidationPrice",
                table: "Liquidations",
                newName: "TotalAsset");

            migrationBuilder.RenameColumn(
                name: "LiquidationForm",
                table: "Liquidations",
                newName: "LiquidationType");

            migrationBuilder.RenameColumn(
                name: "AssetStatus",
                table: "Liquidations",
                newName: "LiquidationId");

            migrationBuilder.AddColumn<int>(
                name: "LiquidationStatus",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LiquidationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetID = table.Column<string>(nullable: true),
                    AssetStatus = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    LiquidationForm = table.Column<int>(nullable: false),
                    LiquidationID = table.Column<string>(nullable: true),
                    LiquidationPrice = table.Column<string>(nullable: true),
                    OriginalPrice = table.Column<float>(nullable: false),
                    ResidualValue = table.Column<float>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidationDetails", x => x.Id);
                });
        }
    }
}

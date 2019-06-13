using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class updatenewmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetDetails");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "AnnualDepreciationRate",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LiquidationDate",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ResidualPrice",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "WarrantyEndDate",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Liquidations",
                newName: "StatusApproved");

            migrationBuilder.RenameColumn(
                name: "LiquidatorName",
                table: "Liquidations",
                newName: "PurchaseUnit");

            migrationBuilder.RenameColumn(
                name: "ContractCode",
                table: "Liquidations",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "LiquidationDetails",
                newName: "AssetStatus");

            migrationBuilder.RenameColumn(
                name: "ProviderID",
                table: "Assets",
                newName: "ProviderId");

            migrationBuilder.RenameColumn(
                name: "TotalRepairCost",
                table: "Assets",
                newName: "OriginalPrice");

            migrationBuilder.RenameColumn(
                name: "TotalMaintenanceCosts",
                table: "Assets",
                newName: "DepreciationValue");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Assets",
                newName: "WarrantyExpiryDate");

            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "Assets",
                newName: "StatusApproved");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Assets",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Assets",
                newName: "Describe");

            migrationBuilder.RenameColumn(
                name: "DepartmentsNeedFollow",
                table: "Assets",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "CateID",
                table: "Assets",
                newName: "Quantity");

            migrationBuilder.AddColumn<float>(
                name: "AmountOfLiquidation",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "LiquidationDate",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiquidationId",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiquidationStatus",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LiquidationType",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LiquidationForm",
                table: "LiquidationDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "OriginalPrice",
                table: "LiquidationDetails",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ResidualValue",
                table: "LiquidationDetails",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "Assets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AssetGrouptId",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetId",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetName",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssetType",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthOfDepreciation",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssetGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetGrouptId = table.Column<string>(nullable: true),
                    AssetGroupName = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetGroupParentId = table.Column<string>(nullable: true),
                    MonthOfDepreciation = table.Column<int>(nullable: false),
                    DepreciationRates = table.Column<float>(nullable: false),
                    StatusApproved = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    ExportDate = table.Column<int>(nullable: false),
                    ExpectedDateRepaired = table.Column<string>(nullable: true),
                    ExpectedRepairUnit = table.Column<string>(nullable: true),
                    Proposer = table.Column<string>(nullable: true),
                    StaffInCharge = table.Column<string>(nullable: true),
                    ExpectedCost = table.Column<float>(nullable: false),
                    ExpectedContent = table.Column<string>(nullable: true),
                    ExpectedNote = table.Column<string>(nullable: true),
                    DateRepaired = table.Column<string>(nullable: true),
                    RepairUnit = table.Column<int>(nullable: false),
                    Cost = table.Column<float>(nullable: false),
                    IsChangeFunction = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Revokes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    RevokeDate = table.Column<string>(nullable: true),
                    AssetId = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    RevokeContent = table.Column<string>(nullable: true),
                    AssetStatus = table.Column<string>(nullable: true),
                    CurrentLocationOfAssets = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revokes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    TransferDate = table.Column<string>(nullable: true),
                    ReceivingUnit = table.Column<string>(nullable: true),
                    Receiver = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StatusApproved = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseAssets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    UnitsUsedId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    OfficialUseDate = table.Column<string>(nullable: true),
                    StartDateOfDepreciation = table.Column<string>(nullable: true),
                    EndDateOfDepreciation = table.Column<string>(nullable: true),
                    DepreciationValueForTheFirstMonth = table.Column<float>(nullable: false),
                    StatusApproved = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseAssets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetGroups");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Revokes");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "UseAssets");

            migrationBuilder.DropColumn(
                name: "AmountOfLiquidation",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "LiquidationDate",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "LiquidationId",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "LiquidationStatus",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "LiquidationType",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "LiquidationForm",
                table: "LiquidationDetails");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "LiquidationDetails");

            migrationBuilder.DropColumn(
                name: "ResidualValue",
                table: "LiquidationDetails");

            migrationBuilder.DropColumn(
                name: "AssetGrouptId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetName",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetType",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "MonthOfDepreciation",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "StatusApproved",
                table: "Liquidations",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "PurchaseUnit",
                table: "Liquidations",
                newName: "LiquidatorName");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Liquidations",
                newName: "ContractCode");

            migrationBuilder.RenameColumn(
                name: "AssetStatus",
                table: "LiquidationDetails",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Assets",
                newName: "ProviderID");

            migrationBuilder.RenameColumn(
                name: "WarrantyExpiryDate",
                table: "Assets",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusApproved",
                table: "Assets",
                newName: "SerialNumber");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Assets",
                newName: "CateID");

            migrationBuilder.RenameColumn(
                name: "OriginalPrice",
                table: "Assets",
                newName: "TotalRepairCost");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Assets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Describe",
                table: "Assets",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DepreciationValue",
                table: "Assets",
                newName: "TotalMaintenanceCosts");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Assets",
                newName: "DepartmentsNeedFollow");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Liquidations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ProviderID",
                table: "Assets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AnnualDepreciationRate",
                table: "Assets",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "LiquidationDate",
                table: "Assets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Assets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "PurchasePrice",
                table: "Assets",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ResidualPrice",
                table: "Assets",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyEndDate",
                table: "Assets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AssetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetID = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });
        }
    }
}

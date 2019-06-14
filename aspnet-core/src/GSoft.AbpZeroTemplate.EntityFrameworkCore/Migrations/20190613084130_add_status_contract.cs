using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class add_status_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Bidding_BiddingId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AbpOrganizationUnits_OrganizationUnitId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "Bidding");

            migrationBuilder.DropIndex(
                name: "IX_Plans_OrganizationUnitId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_BiddingId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Contracts",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "BiddingId",
                table: "Contracts",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "BidProfileId",
                table: "Contracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Contracts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2019, 6, 13, 15, 41, 29, 220, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BidProfileId",
                table: "Contracts",
                column: "BidProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SupplierId",
                table: "Contracts",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_BidProfiles_BidProfileId",
                table: "Contracts",
                column: "BidProfileId",
                principalTable: "BidProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Suppliers_SupplierId",
                table: "Contracts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_BidProfiles_BidProfileId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Suppliers_SupplierId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_BidProfileId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_SupplierId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "BidProfileId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Contracts",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Contracts",
                newName: "BiddingId");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "Plans",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bidding",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BiddingType = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bidding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bidding_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bidding_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2019, 6, 13, 9, 39, 25, 661, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Plans_OrganizationUnitId",
                table: "Plans",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BiddingId",
                table: "Contracts",
                column: "BiddingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bidding_ProductId",
                table: "Bidding",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bidding_SupplierId",
                table: "Bidding",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Bidding_BiddingId",
                table: "Contracts",
                column: "BiddingId",
                principalTable: "Bidding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AbpOrganizationUnits_OrganizationUnitId",
                table: "Plans",
                column: "OrganizationUnitId",
                principalTable: "AbpOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

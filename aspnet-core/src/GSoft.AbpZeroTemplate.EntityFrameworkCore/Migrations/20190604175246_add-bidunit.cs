using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class addbidunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Index",
                table: "BidProfiles",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "BidProfiles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BidUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BidProfileId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SubmitDate = table.Column<DateTime>(nullable: false),
                    BeginCost = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    ProofNum = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidUnit_BidProfiles_BidProfileId",
                        column: x => x.BidProfileId,
                        principalTable: "BidProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidUnit_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidUnit_BidProfileId",
                table: "BidUnit",
                column: "BidProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BidUnit_ProductId",
                table: "BidUnit",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidUnit");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "BidProfiles");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "BidProfiles",
                newName: "Index");
        }
    }
}

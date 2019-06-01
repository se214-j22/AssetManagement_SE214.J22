using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class add_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BiddingId = table.Column<int>(nullable: false),
                    TotalValueOfContract = table.Column<float>(nullable: false),
                    TotalValueOfImplementation = table.Column<float>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    GuaranteeId = table.Column<int>(nullable: false),
                    GaranteeContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Biddings_BiddingId",
                        column: x => x.BiddingId,
                        principalTable: "Biddings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_GaranteeContracts_GaranteeContractId",
                        column: x => x.GaranteeContractId,
                        principalTable: "GaranteeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Guarantees_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "Guarantees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BiddingId",
                table: "Contracts",
                column: "BiddingId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_GaranteeContractId",
                table: "Contracts",
                column: "GaranteeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_GuaranteeId",
                table: "Contracts",
                column: "GuaranteeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}

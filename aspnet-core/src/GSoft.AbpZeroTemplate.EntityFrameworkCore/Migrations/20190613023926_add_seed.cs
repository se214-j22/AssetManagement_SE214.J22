using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSoft.AbpZeroTemplate.Migrations
{
    public partial class add_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "Plans",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Major", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "All Departments" },
                    { 2, null, null, "IT" },
                    { 3, null, null, "HR" },
                    { 4, null, null, "Sale" },
                    { 5, null, null, "PR" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Code", "Name", "Note", "Status" },
                values: new object[] { 1, "PJ001", "Projector", "", 1 });

            migrationBuilder.InsertData(
                table: "SupplierTypes",
                columns: new[] { "Id", "Code", "Name", "Note", "Status" },
                values: new object[] { 1, "SA001", "Electronic", "", 1 });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Code", "Contact", "CreateDate", "Description", "Email", "Fax", "Name", "Phone", "Status", "SupplierTypeId" },
                values: new object[] { 1, null, "DELL001", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "DELL", null, 1, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Address", "CalUnit", "Code", "CreateDate", "Description", "Name", "ProductTypeId", "Status", "SupplierId", "UnitPrice" },
                values: new object[] { 1, null, "unit", "DELL-PJ", new DateTime(2019, 6, 13, 9, 39, 25, 661, DateTimeKind.Local), null, "Dell Projector", 1, 1, 1, 1E+07f });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_OrganizationUnitId",
                table: "Plans",
                column: "OrganizationUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AbpOrganizationUnits_OrganizationUnitId",
                table: "Plans",
                column: "OrganizationUnitId",
                principalTable: "AbpOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AbpOrganizationUnits_OrganizationUnitId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_OrganizationUnitId",
                table: "Plans");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SupplierTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "Plans");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalculator.Data.Migrations
{
    public partial class updatedCalculationTableFieldFromTypeToPostalCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Calculations",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Calculations",
                newName: "UpdatedUserId");

            migrationBuilder.AlterColumn<double>(
                name: "TotalOwedIncomeTax",
                table: "Calculations",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "Calculations",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedDate",
                table: "Calculations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertedUserId",
                table: "Calculations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Calculations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertedDate",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "InsertedUserId",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Calculations");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "Calculations",
                newName: "MyProperty");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Calculations",
                newName: "Type");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOwedIncomeTax",
                table: "Calculations",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Calculations",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

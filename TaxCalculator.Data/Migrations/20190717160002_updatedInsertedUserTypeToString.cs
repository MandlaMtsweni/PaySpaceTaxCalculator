using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalculator.Data.Migrations
{
    public partial class updatedInsertedUserTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InsertedUserId",
                table: "Calculations",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InsertedUserId",
                table: "Calculations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

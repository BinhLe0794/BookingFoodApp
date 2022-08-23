#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminApp.Migrations;

public partial class changeCategoryEnums : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            "Category",
            "Dishes",
            "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            "Category",
            "Dishes",
            "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");
    }
}
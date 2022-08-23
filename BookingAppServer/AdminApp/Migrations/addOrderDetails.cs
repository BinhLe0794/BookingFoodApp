#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminApp.Migrations;

public partial class addOrderDetails : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Dishes_Orders_OrderId",
            "Dishes");

        migrationBuilder.DropIndex(
            "IX_Dishes_OrderId",
            "Dishes");

        migrationBuilder.DropColumn(
            "Quantity",
            "Orders");

        migrationBuilder.DropColumn(
            "Total",
            "Orders");

        migrationBuilder.DropColumn(
            "OrderId",
            "Dishes");

        migrationBuilder.CreateTable(
            "OrderDetails",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Quantity = table.Column<int>("int", nullable: false),
                Total = table.Column<double>("float", nullable: false),
                OrderId = table.Column<Guid>("uniqueidentifier", nullable: false),
                DishId = table.Column<Guid>("uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderDetails", x => x.Id);
                table.ForeignKey(
                    "FK_OrderDetails_Dishes_DishId",
                    x => x.DishId,
                    "Dishes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_OrderDetails_Orders_OrderId",
                    x => x.OrderId,
                    "Orders",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_OrderDetails_DishId",
            "OrderDetails",
            "DishId");

        migrationBuilder.CreateIndex(
            "IX_OrderDetails_OrderId",
            "OrderDetails",
            "OrderId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "OrderDetails");

        migrationBuilder.AddColumn<int>(
            "Quantity",
            "Orders",
            "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<double>(
            "Total",
            "Orders",
            "float",
            nullable: false,
            defaultValue: 0.0);

        migrationBuilder.AddColumn<Guid>(
            "OrderId",
            "Dishes",
            "uniqueidentifier",
            nullable: true);

        migrationBuilder.CreateIndex(
            "IX_Dishes_OrderId",
            "Dishes",
            "OrderId");

        migrationBuilder.AddForeignKey(
            "FK_Dishes_Orders_OrderId",
            "Dishes",
            "OrderId",
            "Orders",
            principalColumn: "Id");
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

namespace Napa.Data.Menu.Migrations
{
    public partial class Orderabe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CategoriesPriceKind",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Categories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CategoriesPriceKind");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Categories");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Napa.Data.Menu.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<System.Int32>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<System.String>(nullable: true),
                    Show = table.Column<System.Boolean>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesPriceKind",
                columns: table => new
                {
                    Id = table.Column<System.Int32>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<System.String>(nullable: true),
                    CategoryId = table.Column<System.Int32>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesPriceKind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriesPriceKind_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<System.Int32>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<System.Int32>(nullable: false),
                    Name = table.Column<System.String>(nullable: true),
                    Description = table.Column<System.String>(nullable: true),
                    Show = table.Column<System.Boolean>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<System.Int32>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<System.Int32>(nullable: false),
                    MenuPriceKindId = table.Column<System.Int32>(nullable: false),
                    Value = table.Column<System.Single>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Items_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prices_CategoriesPriceKind_MenuPriceKindId",
                        column: x => x.MenuPriceKindId,
                        principalTable: "CategoriesPriceKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesPriceKind_CategoryId",
                table: "CategoriesPriceKind",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_MenuItemId",
                table: "Prices",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_MenuPriceKindId",
                table: "Prices",
                column: "MenuPriceKindId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "CategoriesPriceKind");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

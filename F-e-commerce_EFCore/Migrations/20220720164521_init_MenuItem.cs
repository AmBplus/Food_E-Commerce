using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F_e_commerce_EFCore.Migrations
{
    public partial class init_MenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForeignKeyCategory = table.Column<int>(type: "int", nullable: false),
                    ForeignKeyFoodType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Categories_ForeignKeyCategory",
                        column: x => x.ForeignKeyCategory,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItems_FoodTypes_ForeignKeyFoodType",
                        column: x => x.ForeignKeyFoodType,
                        principalTable: "FoodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ForeignKeyCategory",
                table: "MenuItems",
                column: "ForeignKeyCategory");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ForeignKeyFoodType",
                table: "MenuItems",
                column: "ForeignKeyFoodType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");
        }
    }
}

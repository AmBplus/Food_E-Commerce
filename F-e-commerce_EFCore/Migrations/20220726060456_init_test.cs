using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F_e_commerce_EFCore.Migrations
{
    public partial class init_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "test",
                table: "ShopingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "ShopingCarts");
        }
    }
}

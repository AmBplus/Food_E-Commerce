using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F_e_commerce_EFCore.Migrations
{
    public partial class removetest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "ShopingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "test",
                table: "ShopingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

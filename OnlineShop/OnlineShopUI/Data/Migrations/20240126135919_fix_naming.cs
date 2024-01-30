using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix_naming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Part_Id",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "ShoppingCart_Id",
                table: "CartDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Part_Id",
                table: "CartDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCart_Id",
                table: "CartDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class UpdateProductv092815092022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISOCurrencySymbol",
                table: "BusinessCore.Shop.Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visibility",
                table: "BusinessCore.Shop.Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISOCurrencySymbol",
                table: "BusinessCore.Shop.Products");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "BusinessCore.Shop.Products");
        }
    }
}

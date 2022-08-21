using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class UpdateProductv230121082022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeatureImage",
                table: "BusinessCore.Shop.Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedTitle",
                table: "BusinessCore.Shop.Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "BusinessCore.Shop.Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureImage",
                table: "BusinessCore.Shop.Products");

            migrationBuilder.DropColumn(
                name: "NormalizedTitle",
                table: "BusinessCore.Shop.Products");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BusinessCore.Shop.Products");
        }
    }
}

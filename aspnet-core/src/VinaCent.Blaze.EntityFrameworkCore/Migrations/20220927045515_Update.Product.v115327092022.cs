using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class UpdateProductv115327092022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureImage",
                table: "BusinessCore.Shop.Products");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedTitle",
                table: "BusinessCore.Shop.Tags",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedTitle",
                table: "BusinessCore.Shop.Tags");

            migrationBuilder.AddColumn<string>(
                name: "FeatureImage",
                table: "BusinessCore.Shop.Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

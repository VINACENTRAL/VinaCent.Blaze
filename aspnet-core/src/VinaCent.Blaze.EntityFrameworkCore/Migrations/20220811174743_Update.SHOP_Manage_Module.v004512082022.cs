using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class UpdateSHOP_Manage_Modulev004512082022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BusinessCore.Shop.Products",
                type: "decimal(25,12)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,25)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "BusinessCore.Shop.Products",
                type: "decimal(25,12)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,25)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BusinessCore.Shop.Products",
                type: "decimal(25,25)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,12)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "BusinessCore.Shop.Products",
                type: "decimal(25,25)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,12)");
        }
    }
}

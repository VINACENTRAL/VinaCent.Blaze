using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class Update_CurrencyExchangeRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ConvertedValue",
                table: "BusinessCore.CurrencyExchangeRates",
                type: "decimal(25,12)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,25)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ConvertedValue",
                table: "BusinessCore.CurrencyExchangeRates",
                type: "decimal(25,25)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,12)");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class AddCurrencyUnits_CurrencyExchangeRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessCore.CurrencyExchangeRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ISOCurrencySymbolFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISOCurrencySymbolTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.CurrencyExchangeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.CurrencyUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CultureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyEnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyNativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyDecimalDigits = table.Column<int>(type: "int", nullable: false),
                    CurrencyDecimalSeparator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyGroupSeparator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISOCurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyNegativePattern = table.Column<int>(type: "int", nullable: false),
                    CurrencyPositivePattern = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.CurrencyUnits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCore.CurrencyExchangeRates");

            migrationBuilder.DropTable(
                name: "BusinessCore.CurrencyUnits");
        }
    }
}

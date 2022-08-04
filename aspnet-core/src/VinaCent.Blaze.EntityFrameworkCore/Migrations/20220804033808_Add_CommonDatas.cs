using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class Add_CommonDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCardNumber",
                table: "AbpUsers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AbpUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppCore.CommonData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCore.CommonData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCore.CommonData");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IdentityCardNumber",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AbpUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class AddProductImagesv143821082022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerVisibleContent",
                table: "BusinessCore.Shop.Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerVisibleContent",
                table: "BusinessCore.Shop.Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFeature = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductImages_BusinessCore.Shop.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "BusinessCore.Shop.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductImages_ProductId",
                table: "BusinessCore.Shop.ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.ProductImages");

            migrationBuilder.DropColumn(
                name: "BuyerVisibleContent",
                table: "BusinessCore.Shop.Products");

            migrationBuilder.DropColumn(
                name: "SellerVisibleContent",
                table: "BusinessCore.Shop.Products");
        }
    }
}

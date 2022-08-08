using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class AddSHOP_Manage_Modulev1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ConvertedValue",
                table: "BusinessCore.CurrencyExchangeRates",
                type: "decimal(25,25)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.Categories_BusinessCore.Shop.Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BusinessCore.Shop.Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(25,25)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(25,25)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartSellAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndSellAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.CartItems_BusinessCore.Shop.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "BusinessCore.Shop.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductCategories_BusinessCore.Shop.Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BusinessCore.Shop.Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductCategories_BusinessCore.Shop.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "BusinessCore.Shop.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.ProductMetas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.ProductMetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductMetas_BusinessCore.Shop.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "BusinessCore.Shop.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.ProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductReviews_BusinessCore.Shop.ProductReviews_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BusinessCore.Shop.ProductReviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductReviews_BusinessCore.Shop.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "BusinessCore.Shop.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.ProductTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductTags_BusinessCore.Shop.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "BusinessCore.Shop.Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.ProductTags_BusinessCore.Shop.Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "BusinessCore.Shop.Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.CartItems_ProductId",
                table: "BusinessCore.Shop.CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.Categories_ParentId",
                table: "BusinessCore.Shop.Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductCategories_CategoryId",
                table: "BusinessCore.Shop.ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductCategories_ProductId",
                table: "BusinessCore.Shop.ProductCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductMetas_ProductId",
                table: "BusinessCore.Shop.ProductMetas",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductReviews_ParentId",
                table: "BusinessCore.Shop.ProductReviews",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductReviews_ProductId",
                table: "BusinessCore.Shop.ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductTags_ProductId",
                table: "BusinessCore.Shop.ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.ProductTags_TagId",
                table: "BusinessCore.Shop.ProductTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.CartItems");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.ProductCategories");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.ProductMetas");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.ProductReviews");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.ProductTags");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.Categories");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.Products");

            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.Tags");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConvertedValue",
                table: "BusinessCore.CurrencyExchangeRates",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(25,25)");
        }
    }
}

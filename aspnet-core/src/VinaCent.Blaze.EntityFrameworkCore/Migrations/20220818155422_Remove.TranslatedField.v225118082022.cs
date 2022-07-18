using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class RemoveTranslatedFieldv225118082022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCore.TranslateFields");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "BusinessCore.Shop.Categories");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "BusinessCore.Shop.Categories");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BusinessCore.Shop.Categories");

            migrationBuilder.CreateTable(
                name: "BusinessCore.Shop.CategoryTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCore.Shop.CategoryTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCore.Shop.CategoryTranslation_BusinessCore.Shop.Categories_CoreId",
                        column: x => x.CoreId,
                        principalTable: "BusinessCore.Shop.Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCore.Shop.CategoryTranslation_CoreId",
                table: "BusinessCore.Shop.CategoryTranslation",
                column: "CoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCore.Shop.CategoryTranslation");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "BusinessCore.Shop.Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "BusinessCore.Shop.Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BusinessCore.Shop.Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppCore.TranslateFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCore.TranslateFields", x => x.Id);
                });
        }
    }
}

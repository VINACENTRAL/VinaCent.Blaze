using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinaCent.Blaze.Migrations
{
    public partial class Add_User_ListSocialNetwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Background",
                table: "AbpUsers",
                newName: "Cover");

            migrationBuilder.AddColumn<string>(
                name: "ListSocialNetworkRawJson",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListSocialNetworkRawJson",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "Cover",
                table: "AbpUsers",
                newName: "Background");
        }
    }
}

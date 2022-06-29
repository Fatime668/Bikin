using Microsoft.EntityFrameworkCore.Migrations;

namespace Bikin.Migrations
{
    public partial class newCOlumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "SocialMedias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "SocialMedias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "icon",
                table: "SocialMedias");
        }
    }
}

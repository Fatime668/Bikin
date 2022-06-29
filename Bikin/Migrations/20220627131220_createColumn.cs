using Microsoft.EntityFrameworkCore.Migrations;

namespace Bikin.Migrations
{
    public partial class createColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "icon",
                table: "SocialMedias");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SocialMedias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "SocialMedias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "SocialMedias");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "SocialMedias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "SocialMedias",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

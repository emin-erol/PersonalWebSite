using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class configure_Banner_and_SocialMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_Banners_BannerId",
                table: "SocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedias_BannerId",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "BannerId",
                table: "SocialMedias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerId",
                table: "SocialMedias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_BannerId",
                table: "SocialMedias",
                column: "BannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_Banners_BannerId",
                table: "SocialMedias",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "BannerId");
        }
    }
}

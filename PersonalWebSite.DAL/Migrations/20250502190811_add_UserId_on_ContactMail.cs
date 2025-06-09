using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_UserId_on_ContactMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ContactMails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactMails");
        }
    }
}

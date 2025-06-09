using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class remove_UserId_on_ResumeCategoryItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ResumeCategoryItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ResumeCategoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

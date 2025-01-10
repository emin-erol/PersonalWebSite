using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class configure_relationship_between_ResumeCategory_and_ResumeCategoryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeCategoryId",
                table: "ResumeCategoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCategoryItems_ResumeCategoryId",
                table: "ResumeCategoryItems",
                column: "ResumeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCategoryItems_ResumeCategories_ResumeCategoryId",
                table: "ResumeCategoryItems",
                column: "ResumeCategoryId",
                principalTable: "ResumeCategories",
                principalColumn: "ResumeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCategoryItems_ResumeCategories_ResumeCategoryId",
                table: "ResumeCategoryItems");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCategoryItems_ResumeCategoryId",
                table: "ResumeCategoryItems");

            migrationBuilder.DropColumn(
                name: "ResumeCategoryId",
                table: "ResumeCategoryItems");
        }
    }
}

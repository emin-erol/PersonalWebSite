using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWebSite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class remove_Resume_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCategories_Resumes_ResumeId",
                table: "ResumeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCategoryItems_ResumeCategories_ResumeCategoryId",
                table: "ResumeCategoryItems");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_ResumeCategories_ResumeId",
                table: "ResumeCategories");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ResumeCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeCategoryId",
                table: "ResumeCategoryItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCategoryItems_ResumeCategories_ResumeCategoryId",
                table: "ResumeCategoryItems",
                column: "ResumeCategoryId",
                principalTable: "ResumeCategories",
                principalColumn: "ResumeCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeCategoryItems_ResumeCategories_ResumeCategoryId",
                table: "ResumeCategoryItems");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeCategoryId",
                table: "ResumeCategoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "ResumeCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.ResumeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeCategories_ResumeId",
                table: "ResumeCategories",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCategories_Resumes_ResumeId",
                table: "ResumeCategories",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "ResumeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeCategoryItems_ResumeCategories_ResumeCategoryId",
                table: "ResumeCategoryItems",
                column: "ResumeCategoryId",
                principalTable: "ResumeCategories",
                principalColumn: "ResumeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

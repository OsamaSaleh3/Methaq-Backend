using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionTasks_Lectures_LectureId",
                table: "SectionTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionTasks_Lectures_LectureId",
                table: "SectionTasks",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionTasks_Lectures_LectureId",
                table: "SectionTasks");

            

            migrationBuilder.AddForeignKey(
                name: "FK_SectionTasks_Lectures_LectureId",
                table: "SectionTasks",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

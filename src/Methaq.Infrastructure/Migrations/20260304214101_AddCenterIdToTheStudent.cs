using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCenterIdToTheStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CenterId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CenterId",
                table: "Students",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_QuranCenters_CenterId",
                table: "Students",
                column: "CenterId",
                principalTable: "QuranCenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_QuranCenters_CenterId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CenterId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "Students");
        }
    }
}

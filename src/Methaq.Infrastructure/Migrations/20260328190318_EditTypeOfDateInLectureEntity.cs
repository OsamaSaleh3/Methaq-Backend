using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditTypeOfDateInLectureEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Lectures\" " +
                "ALTER COLUMN \"Date\" TYPE date" +
                " USING \"Date\"::date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Lectures\" " +
                "ALTER COLUMN \"Date\" TYPE timestamp with time zone" +
                " USING \"Date\"::timestamp with time zone");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditDateOfBirthType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""AspNetUsers"" 
                  ALTER COLUMN ""DateOfBirth"" TYPE date 
                  USING ""DateOfBirth""::date;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""AspNetUsers"" 
                  ALTER COLUMN ""DateOfBirth"" TYPE timestamp with time zone 
                  USING ""DateOfBirth""::timestamp with time zone;"
            );
        }
    }
}
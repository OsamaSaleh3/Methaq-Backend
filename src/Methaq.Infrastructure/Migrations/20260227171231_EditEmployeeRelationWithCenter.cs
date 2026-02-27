using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditEmployeeRelationWithCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterSupervisors");

            migrationBuilder.RenameColumn(
                name: "ManagedCenterId",
                table: "Employees",
                newName: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CenterId",
                table: "Employees",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_QuranCenters_CenterId",
                table: "Employees",
                column: "CenterId",
                principalTable: "QuranCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_QuranCenters_CenterId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CenterId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "CenterId",
                table: "Employees",
                newName: "ManagedCenterId");

            migrationBuilder.CreateTable(
                name: "CenterSupervisors",
                columns: table => new
                {
                    QuranCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterSupervisors", x => new { x.QuranCenterId, x.SupervisorsId });
                    table.ForeignKey(
                        name: "FK_CenterSupervisors_Employees_SupervisorsId",
                        column: x => x.SupervisorsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterSupervisors_QuranCenters_QuranCenterId",
                        column: x => x.QuranCenterId,
                        principalTable: "QuranCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterSupervisors_SupervisorsId",
                table: "CenterSupervisors",
                column: "SupervisorsId");
        }
    }
}

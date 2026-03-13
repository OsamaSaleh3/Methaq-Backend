using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorEnrollmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupervisorEnrollmentRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    RejectionReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorEnrollmentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisorEnrollmentRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorEnrollmentRequests_QuranCenters_CenterId",
                        column: x => x.CenterId,
                        principalTable: "QuranCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorEnrollmentRequests_CenterId",
                table: "SupervisorEnrollmentRequests",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorEnrollmentRequests_EmployeeId",
                table: "SupervisorEnrollmentRequests",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisorEnrollmentRequests");
        }
    }
}

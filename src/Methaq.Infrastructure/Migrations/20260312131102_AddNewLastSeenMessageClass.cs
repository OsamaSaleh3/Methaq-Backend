using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewLastSeenMessageClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserChatLastReads",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastReadMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GroupChatId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatLastReads", x => new { x.UserId, x.GroupChatId });
                    table.ForeignKey(
                        name: "FK_UserChatLastReads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatLastReads_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatLastReads_GroupChats_GroupChatId1",
                        column: x => x.GroupChatId1,
                        principalTable: "GroupChats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserChatLastReads_GroupMessages_LastReadMessageId",
                        column: x => x.LastReadMessageId,
                        principalTable: "GroupMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChatLastReads_GroupChatId",
                table: "UserChatLastReads",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatLastReads_GroupChatId1",
                table: "UserChatLastReads",
                column: "GroupChatId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatLastReads_LastReadMessageId",
                table: "UserChatLastReads",
                column: "LastReadMessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChatLastReads");
        }
    }
}

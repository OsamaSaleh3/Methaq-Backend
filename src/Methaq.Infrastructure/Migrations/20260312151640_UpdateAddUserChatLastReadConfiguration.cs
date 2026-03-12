using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Methaq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddUserChatLastReadConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChatLastReads_GroupChats_GroupChatId1",
                table: "UserChatLastReads");

            migrationBuilder.DropIndex(
                name: "IX_UserChatLastReads_GroupChatId1",
                table: "UserChatLastReads");

            migrationBuilder.DropColumn(
                name: "GroupChatId1",
                table: "UserChatLastReads");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupChatId1",
                table: "UserChatLastReads",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChatLastReads_GroupChatId1",
                table: "UserChatLastReads",
                column: "GroupChatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatLastReads_GroupChats_GroupChatId1",
                table: "UserChatLastReads",
                column: "GroupChatId1",
                principalTable: "GroupChats",
                principalColumn: "Id");
        }
    }
}

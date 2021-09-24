using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatAPI.Migrations
{
    public partial class RevriteChat1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsersLists_Chats_ChatId",
                table: "ChatUsersLists");

            migrationBuilder.DropIndex(
                name: "IX_ChatUsersLists_ChatId",
                table: "ChatUsersLists");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "ChatsList",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsList_ChatId",
                table: "ChatsList",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsList_Chats_ChatId",
                table: "ChatsList",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsList_Chats_ChatId",
                table: "ChatsList");

            migrationBuilder.DropIndex(
                name: "IX_ChatsList_ChatId",
                table: "ChatsList");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "ChatsList",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsersLists_ChatId",
                table: "ChatUsersLists",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsersLists_Chats_ChatId",
                table: "ChatUsersLists",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

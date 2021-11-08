using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatAPI.Migrations
{
    public partial class PasswordAsRoleAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "ChatUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ChatUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "ChatUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ChatUsers");
        }
    }
}

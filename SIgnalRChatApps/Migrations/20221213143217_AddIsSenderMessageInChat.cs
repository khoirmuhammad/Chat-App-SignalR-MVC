using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIgnalRChatApps.Migrations
{
    public partial class AddIsSenderMessageInChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMessageSender",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMessageSender",
                table: "Chats");
        }
    }
}

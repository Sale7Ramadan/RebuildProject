using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSupportTicketsNavigationToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportMessages_Users_SenderId",
                table: "SupportMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Cities_CityId",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Users_UserId",
                table: "SupportTicket");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "SupportMessages",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportMessages_SenderId",
                table: "SupportMessages",
                newName: "IX_SupportMessages_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportMessages_Users_UserId",
                table: "SupportMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Cities_CityId",
                table: "SupportTicket",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Users_UserId",
                table: "SupportTicket",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportMessages_Users_UserId",
                table: "SupportMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Cities_CityId",
                table: "SupportTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportTicket_Users_UserId",
                table: "SupportTicket");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SupportMessages",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportMessages_UserId",
                table: "SupportMessages",
                newName: "IX_SupportMessages_SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportMessages_Users_SenderId",
                table: "SupportMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Cities_CityId",
                table: "SupportTicket",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTicket_Users_UserId",
                table: "SupportTicket",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

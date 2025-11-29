using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedByRoleToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StatusUpdatedAt",
                table: "Reports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByRole",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UpdatedByUserId",
                table: "Reports",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UpdatedByUserId",
                table: "Reports",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UpdatedByUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UpdatedByUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "StatusUpdatedAt",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UpdatedByRole",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Reports");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Reports");

            migrationBuilder.CreateTable(
                name: "ReportLikes",
                columns: table => new
                {
                    ReportLikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLikes", x => x.ReportLikeId);
                    table.ForeignKey(
                        name: "FK_ReportLikes_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportLikes_ReportId",
                table: "ReportLikes",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportLikes_UserId_ReportId",
                table: "ReportLikes",
                columns: new[] { "UserId", "ReportId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportLikes");

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

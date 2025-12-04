using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class cascadeAddedFoReportAndComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Reports",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Reports",
                table: "Comments",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ReportID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Reports",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Reports",
                table: "Comments",
                column: "ReportID",
                principalTable: "Reports",
                principalColumn: "ReportID");
        }
    }
}

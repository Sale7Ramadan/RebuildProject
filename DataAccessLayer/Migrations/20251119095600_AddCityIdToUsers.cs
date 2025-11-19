using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCityIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Users");
        }
    }
}

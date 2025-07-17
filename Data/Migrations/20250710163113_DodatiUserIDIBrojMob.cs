using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flor.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodatiUserIDIBrojMob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrojMobitela",
                table: "Narudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Narudzbe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_UserId",
                table: "Narudzbe",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzbe_AspNetUsers_UserId",
                table: "Narudzbe",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzbe_AspNetUsers_UserId",
                table: "Narudzbe");

            migrationBuilder.DropIndex(
                name: "IX_Narudzbe_UserId",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "BrojMobitela",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Narudzbe");
        }
    }
}

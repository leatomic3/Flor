using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flor.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodajNarudzbu1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Poruka",
                table: "Narudzbe",
                newName: "Napomena");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Narudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Narudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImePrezime",
                table: "Narudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "ImePrezime",
                table: "Narudzbe");

            migrationBuilder.RenameColumn(
                name: "Napomena",
                table: "Narudzbe",
                newName: "Poruka");
        }
    }
}

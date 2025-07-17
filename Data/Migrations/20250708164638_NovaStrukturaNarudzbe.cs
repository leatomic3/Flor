using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flor.Data.Migrations
{
    /// <inheritdoc />
    public partial class NovaStrukturaNarudzbe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzbe_Buketi_BuketId",
                table: "Narudzbe");

            migrationBuilder.DropIndex(
                name: "IX_Narudzbe_BuketId",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "BuketId",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "Velicina",
                table: "Narudzbe");

            migrationBuilder.AddColumn<decimal>(
                name: "UkupnaCijena",
                table: "Narudzbe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cijena",
                table: "NarudzbaStavke",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UkupnaCijena",
                table: "Narudzbe");

            migrationBuilder.DropColumn(
                name: "Cijena",
                table: "NarudzbaStavke");

            migrationBuilder.AddColumn<int>(
                name: "BuketId",
                table: "Narudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Velicina",
                table: "Narudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_BuketId",
                table: "Narudzbe",
                column: "BuketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzbe_Buketi_BuketId",
                table: "Narudzbe",
                column: "BuketId",
                principalTable: "Buketi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

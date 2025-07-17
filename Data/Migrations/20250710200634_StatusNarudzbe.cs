using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flor.Data.Migrations
{
    /// <inheritdoc />
    public partial class StatusNarudzbe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Obavljeno",
                table: "Narudzbe",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Obavljeno",
                table: "Narudzbe");
        }
    }
}

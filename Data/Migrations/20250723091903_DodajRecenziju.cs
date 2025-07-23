using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flor.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodajRecenziju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Odobrena = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recenzije");
        }
    }
}

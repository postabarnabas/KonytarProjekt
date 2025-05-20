using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posta_Barnabas_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kölcsönzések",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlvasóSzám = table.Column<int>(type: "int", nullable: false),
                    LeltáriSzám = table.Column<int>(type: "int", nullable: false),
                    KölcsönzésDátuma = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisszahozásDátuma = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kölcsönzések", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Könyvek",
                columns: table => new
                {
                    LeltáriSzám = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cím = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Szerző = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kiadó = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KiadásÉve = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Könyvek", x => x.LeltáriSzám);
                });

            migrationBuilder.CreateTable(
                name: "Olvasók",
                columns: table => new
                {
                    OlvasóSzám = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Név = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lakcím = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Olvasók", x => x.OlvasóSzám);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kölcsönzések");

            migrationBuilder.DropTable(
                name: "Könyvek");

            migrationBuilder.DropTable(
                name: "Olvasók");
        }
    }
}

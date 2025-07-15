using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarea4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    ProvinciaPK = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProvinciaNombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.ProvinciaPK);
                });

            migrationBuilder.CreateTable(
                name: "Cantones",
                columns: table => new
                {
                    CantonPK = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CantonNombre = table.Column<string>(type: "TEXT", nullable: false),
                    ProvinciaFK = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantones", x => x.CantonPK);
                    table.ForeignKey(
                        name: "FK_Cantones_Provincias_ProvinciaFK",
                        column: x => x.ProvinciaFK,
                        principalTable: "Provincias",
                        principalColumn: "ProvinciaPK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distritos",
                columns: table => new
                {
                    DistritoPK = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DistritoNombre = table.Column<string>(type: "TEXT", nullable: false),
                    CantonFK = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distritos", x => x.DistritoPK);
                    table.ForeignKey(
                        name: "FK_Distritos_Cantones_CantonFK",
                        column: x => x.CantonFK,
                        principalTable: "Cantones",
                        principalColumn: "CantonPK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cantones_ProvinciaFK",
                table: "Cantones",
                column: "ProvinciaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Distritos_CantonFK",
                table: "Distritos",
                column: "CantonFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Distritos");

            migrationBuilder.DropTable(
                name: "Cantones");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clasificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasificaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plataformas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataformas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    ResetToken = table.Column<string>(type: "text", nullable: true),
                    TokenExpiracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juegos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Portada = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Desarrollador = table.Column<string>(type: "text", nullable: false),
                    Editor = table.Column<string>(type: "text", nullable: false),
                    ClasificacionId = table.Column<int>(type: "integer", nullable: false),
                    FechaDeLanzamiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juegos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Juegos_Clasificaciones_ClasificacionId",
                        column: x => x.ClasificacionId,
                        principalTable: "Clasificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    JuegoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagenes_Juegos_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuegoGenero",
                columns: table => new
                {
                    GenerosId = table.Column<int>(type: "integer", nullable: false),
                    JuegosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuegoGenero", x => new { x.GenerosId, x.JuegosId });
                    table.ForeignKey(
                        name: "FK_JuegoGenero_Generos_GenerosId",
                        column: x => x.GenerosId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuegoGenero_Juegos_JuegosId",
                        column: x => x.JuegosId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuegoIdioma",
                columns: table => new
                {
                    IdiomasId = table.Column<int>(type: "integer", nullable: false),
                    JuegosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuegoIdioma", x => new { x.IdiomasId, x.JuegosId });
                    table.ForeignKey(
                        name: "FK_JuegoIdioma_Idiomas_IdiomasId",
                        column: x => x.IdiomasId,
                        principalTable: "Idiomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuegoIdioma_Juegos_JuegosId",
                        column: x => x.JuegosId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuegoPlataforma",
                columns: table => new
                {
                    JuegosId = table.Column<int>(type: "integer", nullable: false),
                    PlataformasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuegoPlataforma", x => new { x.JuegosId, x.PlataformasId });
                    table.ForeignKey(
                        name: "FK_JuegoPlataforma_Juegos_JuegosId",
                        column: x => x.JuegosId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuegoPlataforma_Plataformas_PlataformasId",
                        column: x => x.PlataformasId,
                        principalTable: "Plataformas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    JuegoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Juegos_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juegos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clasificaciones",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Adecuado para todas las edades", "PEGI 3" },
                    { 2, "Adecuado para mayores de 7 años", "PEGI 7" },
                    { 3, "Adecuado para mayores de 12 años", "PEGI 12" },
                    { 4, "Adecuado para mayores de 16 años", "PEGI 16" },
                    { 5, "Adecuado para mayores de 18 años", "PEGI 18" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Accion" },
                    { 2, "Aventura" },
                    { 3, "Fantasia" },
                    { 4, "Terror" },
                    { 5, "Survival" },
                    { 6, "Survival Horror" },
                    { 7, "RPG" },
                    { 8, "Shooter" },
                    { 9, "Estrategia" },
                    { 10, "Simulacion" },
                    { 11, "Deportes" },
                    { 12, "Carreras" },
                    { 13, "Lucha" },
                    { 14, "Plataformas" },
                    { 15, "Puzle" },
                    { 16, "Sandbox" },
                    { 17, "Musica" },
                    { 18, "Arcade" },
                    { 19, "Indie" },
                    { 20, "Multijugador" }
                });

            migrationBuilder.InsertData(
                table: "Idiomas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Ingles" },
                    { 2, "Español" },
                    { 3, "Frances" },
                    { 4, "Aleman" },
                    { 5, "Italiano" },
                    { 6, "Portugues" },
                    { 7, "Ruso" },
                    { 8, "Japones" },
                    { 9, "Chino" }
                });

            migrationBuilder.InsertData(
                table: "Plataformas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "PlayStation" },
                    { 2, "Xbox" },
                    { 3, "Nintendo Switch" },
                    { 4, "PC" },
                    { 5, "Game Boy Advance" },
                    { 6, "Android" },
                    { 7, "iOS" },
                    { 8, "Steam Deck" },
                    { 9, "Google Stadia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagenes_JuegoId",
                table: "Imagenes",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoGenero_JuegosId",
                table: "JuegoGenero",
                column: "JuegosId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoIdioma_JuegosId",
                table: "JuegoIdioma",
                column: "JuegosId");

            migrationBuilder.CreateIndex(
                name: "IX_JuegoPlataforma_PlataformasId",
                table: "JuegoPlataforma",
                column: "PlataformasId");

            migrationBuilder.CreateIndex(
                name: "IX_Juegos_ClasificacionId",
                table: "Juegos",
                column: "ClasificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_JuegoId",
                table: "Videos",
                column: "JuegoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagenes");

            migrationBuilder.DropTable(
                name: "JuegoGenero");

            migrationBuilder.DropTable(
                name: "JuegoIdioma");

            migrationBuilder.DropTable(
                name: "JuegoPlataforma");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "Plataformas");

            migrationBuilder.DropTable(
                name: "Juegos");

            migrationBuilder.DropTable(
                name: "Clasificaciones");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libreria.Migrations
{
    /// <inheritdoc />
    public partial class SeModificoModeloLibro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutorIdAutor",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeneroIdGenero",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutorIdAutor",
                table: "Libros",
                column: "AutorIdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_GeneroIdGenero",
                table: "Libros",
                column: "GeneroIdGenero");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autores_AutorIdAutor",
                table: "Libros",
                column: "AutorIdAutor",
                principalTable: "Autores",
                principalColumn: "IdAutor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Generos_GeneroIdGenero",
                table: "Libros",
                column: "GeneroIdGenero",
                principalTable: "Generos",
                principalColumn: "IdGenero",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autores_AutorIdAutor",
                table: "Libros");

            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Generos_GeneroIdGenero",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_AutorIdAutor",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_GeneroIdGenero",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "AutorIdAutor",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "GeneroIdGenero",
                table: "Libros");
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public partial class Libro
{
    [Key]
    public int IdLibro { get; set; }
    public string Titulo { get; set; } = null!;
    public decimal Precio { get; set; }
    public string? ImagenUrl { get; set; }

    public int IdAutor { get; set; }
    public int IdGenero { get; set; }

    public Autor Autor { get; set; } = null!;
    public Genero Genero { get; set; } = null!;
}
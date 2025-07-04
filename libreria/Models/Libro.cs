using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria.Models;

public class Libro
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdLibro { get; set; }

    [Required]
    public string Titulo { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? ImagenUrl { get; set; }

    [Required]
    public int IdAutor { get; set; }

    [Required]
    public int IdGenero { get; set; }
}
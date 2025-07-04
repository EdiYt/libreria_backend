using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public partial class Autor
{
    [Key]
    public int IdAutor { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Biografia { get; set; }
}
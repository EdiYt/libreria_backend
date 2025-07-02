namespace libreria.Models
{
    public partial class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Biografia { get; set; }
    }
}

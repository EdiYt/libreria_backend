using System.ComponentModel.DataAnnotations;

namespace libreria.Models
{
    public partial class Genero
    {
        [Key]
        public int IdGenero {  get; set; }
        public string Nombre { get; set; } = null;

    }
}

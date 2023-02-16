using System.ComponentModel.DataAnnotations;

namespace GESIIMO.Model
{
    public class Torre
    {
        [Key]
        public int IdTorre { get; set; }

        public int IdConjunto { get; set; }

        [Required(ErrorMessage = "Campo Descripción es requerido")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }
    }
}

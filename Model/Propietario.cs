using System.ComponentModel.DataAnnotations;

namespace GESIIMO.Model
{
    public class Propietario
    {
        public int IdPropietario { get; set; }

        public int CantApartamentos { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "Apellido es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Email es requerido")]
        [EmailAddress(ErrorMessage = "Email no es válido")]
        public string Email { get; set; }

        public string Celular { get; set; }

        public bool Estado { get; set; }
    }
}

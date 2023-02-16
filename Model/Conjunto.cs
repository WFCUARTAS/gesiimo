using System.ComponentModel.DataAnnotations;

namespace GESIIMO.Model
{
    public class Conjunto
    {
        [Key]
        public int IdConjunto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo Ciudad es requerido")]
        public int IdCiudad { get; set; }

        public string DescCiudad { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Campo Departamento es requerido")]
        public int IdDepartamento { get; set; }

        public string DescDepartamento { get; set; }

        public string DescUbicacion { get; set; }

        [Required(ErrorMessage = "Campo Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo Dirección es requerido")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Campo Teléfono es requerido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Campo Celular es requerido")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Campo Administrador es requerido")]
        public string Administrador { get; set; }

        [Required(ErrorMessage = "Campo Email es requerido")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; }

        public string Logo { get; set; }

        public bool Estado { get; set; }

        public int CantApartamentos { get; set; }

    }
}

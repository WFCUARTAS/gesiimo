using System.ComponentModel.DataAnnotations;

namespace GESIIMO.Model
{
    public class Apartamento
    {
        [Key]
        public int IdApartamento { get; set; }

        //[Required(ErrorMessage = "Campo Torre es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo Torre es requerido")]
        public int IdTorre { get; set; }
        
        public string DescTorre { get; set; }

        [Required(ErrorMessage = "Campo Descripción es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Coeficiente es requerido")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(typeof(double), "0,01", "100", ErrorMessage = "Valor inválido")]
        public double Coeficiente { get; set; }
        
        public string CodNovedad { get; set; }
     
        public bool Estado { get; set; }

        public string IdUsuario { get; set; }

        public string Propietario { get; set; }
    }
}

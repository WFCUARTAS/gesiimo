using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class OpcionPregunta
    {
      
        public int IdOpcionpregunta { get; set; }

        public int IdPregunta { get; set; }

        [Required(ErrorMessage = "Campo Opcion es requerido")]
        public string Opcion { get; set; }

        public bool Estado { get; set; }

        public string Descripcionpregunta { get; set; }
    }
}

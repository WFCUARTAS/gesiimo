using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GESIIMO.Model
{
    public class AsignacionApartamento
    {
        public int IdApartamentoPropietario { get; set; }
        public int IdPropietario { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Conjunto es requerido.")]
        public int IdConjunto { get; set; }
        public string Conjunto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Torre es requerida.")] 
        public int IdTorre { get; set; }

        public string Torre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Apartamento es requerido.")] 
        public int IdApartamento { get; set; }

        public string Apartamento { get; set; }

        public double Coeficiente { get; set; }

        public bool Estado { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class Poder
    {
        public int IdPoder { get; set; }

        public int IdConjunto { get; set; }

        public int IdTorre { get; set; }

        public int IdApartamento { get; set; }

        public int IdRepresentante { get; set; }

        public int IdRepresentado { get; set; }

        public int IdAsamblea { get; set; }

        public bool Estado { get; set; }

        public string Asamblea { get; set; }

        public string DesAsamblea { get; set; }

        public string Representante { get; set; }

        public string Representado { get; set; }
        public string Observacion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo Apartamento representante es requerido")]
        public int idApartamentoRepresentante { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo Apartamento representado es requerido")]
        public int IdApartamentoRepresentado { get; set; }
    }
}

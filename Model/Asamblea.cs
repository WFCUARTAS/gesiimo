using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class Asamblea
    {
        public int IdAsamblea { get; set; }

        public int CantCuestionarios{ get; set; }

        public int CantPoderes { get; set; }

        public int CantArchivos { get; set; }

        public int IdConjunto { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Estado { get; set; }

        public bool EnvioMasivo { get; set; }

        public string UrlZoom { get; set; }

        public DateTime FechaInicioReal { get; set; }

        public string OrdenDia { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class AccesoAsamblea
    {
        public int idAccesoAsamblea { get; set; }
        public int idAsamblea { get; set; }
        public string IdUsuario { get; set; }
        public double CoeficienteTotal { get; set; }
        public bool EstadoConexion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime UltimaConexion { get; set; }
        public bool TerminosCondiciones { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class Respuesta
    {
        public int idRespuesta { get; set; }
        public int idOpcionPregunta { get; set; }
        public int idAccesoAsamblea { get; set; }
        public DateTime FechaRespuesta { get; set; }
    }
}

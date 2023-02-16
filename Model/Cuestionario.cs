using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class Cuestionario
    {
        public int IdCuestionario { get; set; }
        public int IdAsamblea { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int CantPreguntas { get; set; }
        public IEnumerable<Pregunta> Preguntas { get; set; }
    }
}

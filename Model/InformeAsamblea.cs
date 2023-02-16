using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class InformeAsamblea
    {
        public int IdPregunta { get; set; }
        public string Pregunta { get; set; }
        public int TipoGrafica { get; set; }
        public int idOpcionPregunta { get; set; }
        public string Opcion { get; set; }
        public double Coeficiente { get; set; }
        public int Cantidad { get; set; }
        public string InformePregunta { get; set; }
    }
}

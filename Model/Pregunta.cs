using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class Pregunta
    {
        public int IdPregunta { get; set; }
        public int IdCuestionario { get; set; }

        public int CantOpciones { get; set; }
        public string Cuestionario { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo tipo pregunta es requerido")]
        public int TipoPregunta { get; set; }
        public string DescripcionTipoPregunta { get; set; }

        [Required(ErrorMessage = "Campo pregunta es requerido")]
        public string PreguntaDescripcion { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "Campo tipo gráfica es requerido")]
        public int TipoGrafica { get; set; }

        
        public string DescripcionTipoGrafica { get; set; }
        public int OrdenVisualizacion { get; set; }
        public string DescripcionOrdenVisualizacion { get; set; }
        public bool Prioridad { get; set; }
        public bool Estado { get; set; }
        public int NRespuesta { get; set; }
        public IEnumerable<OpcionPregunta> Opciones { get; set; }
        public int OpcionSeleccionada { get; set; }
        public bool EstadoRespuesta { get; set; }

        public DateTime FechaActivacion { get; set; }
        public DateTime FechaActualServer { get; set; }
        public int Tiempo { get; set; }
        public bool IndRespondida { get; set; }
    }
}

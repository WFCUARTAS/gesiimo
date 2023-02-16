using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class CitacionAsamblea
    {
        public int idCitacionAsamblea { get; set; }
        public int idAsamblea { get; set; }
        public string idUsuario { get; set; }
        public DateTime FechaEnvio { get; set; }

    }
}

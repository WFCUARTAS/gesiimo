using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class Quorum
    {
        
        public int idQuorum { get; set; }
        public int idAsamblea { get; set; }
        public double QuorumTota { get; set; }
        public double QuorumAsistentes { get; set; }
        public int CantidadAsistentes { get; set; }
        public double QuorumAusentes { get; set; }
        public int CantidadAusentes { get; set; }
        public DateTime Fecha { get; set; }


        public string Descripcion { get; set; }
        public double ValorQuorum { get; set; }
        public int Cantidad { get; set; }

    }
}

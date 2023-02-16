using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class EnvioCorreo
    {
        public int IdApartamentoPropietario { get; set; }

        public int IdApartamento { get; set; }

        public string IdUsuario { get; set; }

        public int IdConjunto { get; set; }

        public int IdPropietario { get; set; }

        public string Propietario { get; set; }

        public string Email { get; set; }

        public string Conjunto { get; set; }

        public bool IndCorreoEnviado { get; set; }
    }
}

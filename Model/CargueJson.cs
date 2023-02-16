using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Model
{
    public class CargueJson
    {
        [Range(1, int.MaxValue, ErrorMessage = "Conjunto es requerido.")]
        public int IdConjunto { get; set; }

        [Required(ErrorMessage = "Cadena JSON es requerida.")]
        public string DataJson { get; set; }

    }
}

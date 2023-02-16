using System.ComponentModel.DataAnnotations;

namespace GESIIMO.Model
{
    public class Lista
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string CodNovedad { get; set; }

        public bool Estado { get; set; }

        public int OrdenPriorizacion { get; set; }

    }
}

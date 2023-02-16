using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IRespuestaService
    {
        Task<Respuesta> GetRespuesta(int idRespuesta);

        Task<int> SetRespuesta(Respuesta respuesta);

        Task<IEnumerable<Votacion>> GetVotacion(int idAsamblea);

    }
}

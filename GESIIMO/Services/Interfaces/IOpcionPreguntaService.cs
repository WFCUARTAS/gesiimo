using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Model;


namespace GESIIMO.Services
{
    public interface IOpcionPreguntaService
    {
        Task<IEnumerable<OpcionPregunta>> GetOpcionPreguntas(int? idOpcionpregunta);

        Task<OpcionPregunta> GetOpcionPregunta(int idOpcionpregunta);

        Task<int> SetOpcionPregunta(OpcionPregunta opcionpregunta);

        Task<int> DelOpcionPregunta(int idOpcionpregunta);
    }
}

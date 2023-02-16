using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Model;

namespace GESIIMO.Services
{
    public interface IPreguntaService
    {
        Task<IEnumerable<Pregunta>> GetPreguntas(int idPregunta);
        Task<IEnumerable<Pregunta>> GetPreguntasRespuesta(int idAsamblea, int idAccesoAsamblea);
        Task<bool> GetContestoPregunta(int idPregunta, int idAccesoAsamblea);
        Task<Pregunta> GetPregunta(int idPregunta);

        Task<int> SetPregunta(Pregunta pregunta);
        Task<int> ActivarPregunta(Pregunta pregunta);

        Task<int> DelPregunta(int idPregunta);
    }
}

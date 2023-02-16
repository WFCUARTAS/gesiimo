using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Model;

namespace GESIIMO.Services
{
    public interface ICuestionarioService
    {
        Task<IEnumerable<Cuestionario>> GetCuestionarios(int idCuestionario);

        Task<Cuestionario> GetCuestionario(int idCuestionario);

        Task<int> SetCuestionario(Cuestionario cuestionario);

        Task<int> DelCuestionario(int idCuestionario);
    }
}

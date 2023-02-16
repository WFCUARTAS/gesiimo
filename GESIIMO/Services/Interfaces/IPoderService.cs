using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IPoderService
    {
        Task<IEnumerable<Poder>> GetPoderes(int idAsamblea);

        Task<Poder> GetPoder(int idPoder);

        Task<int> SetPoder(Poder poder);

        Task<int> DelPoder(int idPoder);

        Task<bool> ValidarPoder(int idAsamblea, string idUsuario);
    }
}

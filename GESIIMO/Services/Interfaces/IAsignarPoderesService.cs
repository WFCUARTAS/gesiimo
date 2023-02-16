using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IAsignarPoderesService 
    {
        Task<AsignarPoderes> GetRepresentante(int idApartamento);

        Task<IEnumerable<AsignarPoderes>> GetApartamentoRepresentado(int idTorre, int idAsamblea);

        Task<IEnumerable<AsignarPoderes>> GetApartamentoRepresentante(int idPropietario, int idTorre, int idAsamblea);
    }
}

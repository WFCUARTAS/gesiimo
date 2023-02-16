using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IConjuntoService
    {
        Task<IEnumerable<Conjunto>> GetConjuntos(int idDepartamento, int idCiudad);

        Task<Conjunto> GetConjunto(int idConjunto);

        Task<int> SetConjunto(Conjunto conjunto);

        Task<int> DelConjunto(int idConjunto);

        Task<IEnumerable<EnvioCorreo>> GetCorreos(Asamblea asamblea);
    }
}
using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Repository.Interfaces
{
    public interface IConjuntoRepository
    {
        Task<IEnumerable<Conjunto>> GetConjuntos(int? idCiudad);

        Task<Conjunto> GetConjunto(int idConjunto);

        Task<int> SetConjunto(string action, Conjunto conjunto);

        Task<int> DelConjunto(int idConjunto);
    }
}
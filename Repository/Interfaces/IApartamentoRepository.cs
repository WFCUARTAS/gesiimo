using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Repository.Interfaces
{
    public interface IApartamentoRepository
    {
        Task<IEnumerable<Apartamento>> GetApartamentos(int? idTorre);

        Task<Apartamento> GetApartamento(int idApartamento);

        Task<int> SetApartamento(string action, Apartamento apartamento);

        Task<int> DelApartamento(int idApartamento);
    }
}

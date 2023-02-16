using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IApartamentoService
    {
        Task<IEnumerable<Apartamento>> GetApartamentos(int? idTorre, int idConjunto);
        
        Task<Apartamento> GetApartamento(int idApartamento);
        
        Task<int> SetApartamento(Apartamento apartamento);

        Task<int> DelApartamento(int idApartamento);
    }
}

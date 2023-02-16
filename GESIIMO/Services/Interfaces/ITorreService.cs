using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface ITorreService
    {
        Task<IEnumerable<Torre>> GetTorres(int idConjunto);

        Task<Torre> GetTorre(int idTorre);

        Task<int> SetTorre(Torre torre);

        Task<int> DelTorre(int idTorre);
    }
}
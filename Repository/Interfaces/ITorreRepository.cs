using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Repository.Interfaces
{
    public interface ITorreRepository
    {
        Task<IEnumerable<Torre>> GetTorres(int idConjunto);

        Task<Torre> GetTorre(int idTorre);

        Task<int> SetTorre(string action, Torre torre);

        Task<int> DelTorre(int idTorre);
    }
}

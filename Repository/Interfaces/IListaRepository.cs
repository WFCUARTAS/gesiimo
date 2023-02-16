using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Repository.Interfaces
{
    public interface IListaRepository
    {
        Task<IEnumerable<Lista>> GetLista(string nombre, int? id, bool? estado);

    }
}

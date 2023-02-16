using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IListaService
    {
        Task<IEnumerable<Lista>> GetLista(string nombre, int? id, string codNovedad, bool? estado);

    }
}
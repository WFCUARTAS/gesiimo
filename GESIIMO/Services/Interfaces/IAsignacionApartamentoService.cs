using GESIIMO.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IAsignacionApartamentoService
    {
        Task<IEnumerable<AsignacionApartamento>> GetAsignacionApartamentos(int? idPropietario);

        Task<AsignacionApartamento> GetAsignacionApartamento(int idPropietario);

        Task<int> SetAsignacionApartamentos(AsignacionApartamento apartamentopropietario);
        Task<int> SetCambioEstado(AsignacionApartamento apartamentopropietario);

        Task<int> DelAsignacionApartamentos(int idPropietario);

    }
}

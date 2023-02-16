using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Model;

namespace GESIIMO.Services
{
    public interface IPropietarioService
    {
        Task<IEnumerable<Propietario>> GetPropietarios(int? idPropietario);

        Task<Propietario> GetPropietario(int idPropietario);

        Task<Propietario> GetPropietarioUsuario(string idPropietario);

        Task<int> SetPropietario(Propietario propietario);

        Task<int> DelPropietario(int idPropietario);
    }
}

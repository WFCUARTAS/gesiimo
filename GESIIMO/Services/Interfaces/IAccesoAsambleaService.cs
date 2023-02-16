using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IAccesoAsambleaService
    {
        Task<int> SetAccesoAsamblea(AccesoAsamblea acceso);

        Task<IEnumerable<AccesoAsamblea>> GetAccesosActivos(int idAsamblea);

        Task<IEnumerable<AccesoAsamblea>> GetAccesoAsamblea(int idAsamblea, string idUsuario);

        Task<int> UpdateConexion(AccesoAsamblea acceso);

    }
}

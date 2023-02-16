using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IConexionService
    {
        Task<IEnumerable<Conexion>> GetConexiones(int? IdAsamblea);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Model;

namespace GESIIMO.Services
{
    public interface IConfiguracionService
    {
        Task<Configuracion> GetConfiguracion(string Descripcion);
        Task<int> SetConfiguracion(Configuracion configuracion);

    }
}

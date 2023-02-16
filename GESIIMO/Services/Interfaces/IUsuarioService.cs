using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IUsuarioService
    {
        Task<string> GenerarEmail(Apartamento apartamento, int idConjunto);

    }
}

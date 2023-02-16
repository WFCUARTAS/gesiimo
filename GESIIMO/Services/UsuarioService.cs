using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GESIIMO.Services
{
    public class UsuarioService : IUsuarioService
    {
        public async Task<string> GenerarEmail(Apartamento ap, int idConjunto)
        {
            Random aleatorio = new Random();

            string email = "Apartamento" + ap.Descripcion + "-Torre" + ap.IdTorre + "-" + aleatorio.Next(1, 1000) + "@conjunto" + idConjunto + ".com";

            return email;
        }
    }
}

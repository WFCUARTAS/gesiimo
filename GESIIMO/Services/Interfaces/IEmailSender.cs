using GESIIMO.Data;
using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);

        Task<int> CitacionIndividual(EnvioCorreo propietario, Asamblea asamblea, Conjunto conjunto, string url);
        Task<int> CitacionMasiva(IEnumerable<EnvioCorreo> propietarios, Asamblea asamblea, Conjunto conjunto, string url);

    }
}

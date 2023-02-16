using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IAsambleaService
    {
        Task<IEnumerable<Asamblea>> GetAsambleas(int? idConjunto);

        Task<Asamblea> GetAsamblea(int idAsamblea);

        Task<int> SetAsamblea(Asamblea idAsamblea);        
        Task<Asamblea> SetNewAsamblea(Asamblea asamblea);
        Task<IEnumerable<Quorum>> GetQuorum(int idAsamblea);
        Task<int> SetQuorum(int idAsamblea);
        Task<Quorum> GetQuorumInforme(int idAsamblea);
        Task<IEnumerable<Archivo>> BorrarDatos(int idConjunto);
    }
}

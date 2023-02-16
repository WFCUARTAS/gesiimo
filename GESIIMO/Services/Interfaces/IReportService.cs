using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IReportService
    {
        Task<DataTable> GetInformeVotaciones(int idAsamblea);

        Task<DataTable> GetInfomeActualizacionDatos(int idAsamblea);
        Task<DataTable> GetInfomePoderes(int idAsamblea);
        Task<DataTable> GetInfomeQuorum(int idAsamblea, int idConjunto);
        Task<MemoryStream> GetInfomeAsamblea(int idAsamblea);
    }
}

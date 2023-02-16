using GESIIMO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public interface IArchivoService
    {
        
        Task<IEnumerable<Archivo>> GetArchivos(int? IdAsamblea);

        Task<Archivo> GetArchivo(int idArchivo);

        Task<int> SetArchivo(Archivo archivo);

        Task<int> DelArchivo(int idArchivo);
    }
}

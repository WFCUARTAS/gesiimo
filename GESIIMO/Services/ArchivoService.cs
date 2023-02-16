using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Services.Interfaces;

namespace GESIIMO.Services
{
    public class ArchivoService : IArchivoService
    {
        private readonly SqlConfiguration configuration;
        public ArchivoService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }
        
        public async Task<IEnumerable<Archivo>> GetArchivos(int? IdAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Archivo_GET_DT";

            var parametros = new
            {
                idAsamblea = IdAsamblea
            };


            return await db.QueryAsync<Archivo>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public Task<Archivo> GetArchivo(int idArchivo)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> SetArchivo(Archivo archivo)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Archivo_SET";

            var parametros = new
            {
                IdArchivo  = archivo.IdArchivo,
                IdAsamblea = archivo.IdAsamblea,
                NombreArchivo = archivo.NombreArchivo,
                RutaArchivo = archivo.RutaArchivo,
                Action = archivo.IdArchivo == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> DelArchivo(int idArchivo)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Archivo_SET";

            var parametros = new
            {
                IdArchivo  = idArchivo,
                IdAsamblea = 0,
                NombreArchivo = "",
                RutaArchivo = "",
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

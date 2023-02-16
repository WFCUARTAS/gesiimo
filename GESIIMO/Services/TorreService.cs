using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class TorreService : ITorreService
    {
        private readonly SqlConfiguration configuration;

        public TorreService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<Torre>> GetTorres(int idConjunto)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Torre_GET_MS";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            try
            {
                return await db.QueryAsync<Torre>(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Torre> GetTorre(int idTorre)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Torre_GET_DT";

            var parametros = new
            {
                IdTorre = idTorre
            };

            return await db.QuerySingleAsync<Torre>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetTorre(Torre torre)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Torre_SET";

            var parametros = new
            {
                IdTorre = torre.IdTorre,
                IdConjunto = torre.IdConjunto,
                Descripcion = torre.Descripcion,
                Estado = torre.Estado,
                Action = torre.IdTorre == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> DelTorre(int idTorre)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Torre_SET";

            var parametros = new
            {
                IdTorre = idTorre,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GESIIMO.Model;
using GESIIMO.Repository.Interfaces;

namespace GESIIMO.Repository
{
    public class TorreRepository : ITorreRepository
    {
        private string ConnectionString;

        public TorreRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Torre>> GetTorres(int idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Torre_GET_MS";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            return await db.QueryAsync<Torre>(sql, parametros, null, null, CommandType.StoredProcedure);
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

        public async Task<int> SetTorre(string action, Torre torre)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Torre_SET";

            var parametros = new
            {
                IdTorre = torre.IdTorre,
                IdConjunto = torre.IdConjunto,
                Descripcion = torre.Descripcion,
                Estado = torre.Estado,
                Action = action
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

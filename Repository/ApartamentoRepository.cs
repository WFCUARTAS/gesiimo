using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GESIIMO.Model;
using GESIIMO.Repository.Interfaces;

namespace GESIIMO.Repository
{
    public class ApartamentoRepository : IApartamentoRepository
    {
        private string ConnectionString;

        public ApartamentoRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Apartamento>> GetApartamentos(int? idTorre)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Apartamento_GET_MS";

            var parametros = new
            {
                IdTorre = idTorre
            };

            return await db.QueryAsync<Apartamento>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Apartamento> GetApartamento(int idApartamento)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Apartamento_GET_DT";

            var parametros = new
            {
                IdApartamento = idApartamento
            };

            return await db.QuerySingleAsync<Apartamento>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetApartamento(string action, Apartamento apartamento)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Apartamento_SET";

            var parametros = new
            {
                IdApartamento = apartamento.IdApartamento,
                IdTorre = apartamento.IdTorre,
                Descripcion = apartamento.Descripcion,
                Coeficiente = apartamento.Coeficiente,
                CodNovedad = apartamento.CodNovedad,
                Estado = apartamento.Estado,
                Action = action
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> DelApartamento(int idApartamento)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Apartamento_SET";

            var parametros = new
            {
                IdApartamento = idApartamento,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

    }
}

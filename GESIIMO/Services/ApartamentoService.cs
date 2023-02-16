using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class ApartamentoService : IApartamentoService
    {
        private readonly SqlConfiguration configuration;

        public ApartamentoService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<Apartamento>> GetApartamentos(int? idTorre, int idConjunto)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Apartamento_GET_MS";

            var parametros = new
            {
                IdTorre = idTorre,
                IDConjunto = idConjunto
            };

            return await db.QueryAsync<Apartamento>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Apartamento> GetApartamento(int idApartamento)
        {
            using  SqlConnection db = dbConnection();
            string sql = "dbo.sp_Apartamento_GET_DT";

            var parametros = new
            {
                IdApartamento = idApartamento
            };

            return await db.QuerySingleAsync<Apartamento>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetApartamento(Apartamento apartamento)
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
                IdUsuario = apartamento.IdUsuario,
                Action = apartamento.IdApartamento == 0 ? "POST" : "PUT"
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

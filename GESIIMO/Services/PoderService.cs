using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class PoderService : IPoderService
    {
        private readonly SqlConfiguration configuration;

        public PoderService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }
        public Task<int> DelPoder(int idPoder)
        {
            throw new NotImplementedException();
        }

        public async Task<Poder> GetPoder(int idPoder)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Poder_GET_DT";

            var parametros = new
            {
                IdPoder = idPoder
            };

            return await db.QuerySingleAsync<Poder>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Poder>> GetPoderes(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Poder_GET_MS";

            var parametros = new
            {
                IdAsamblea = idAsamblea
            };

            return await db.QueryAsync<Poder>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetPoder(Poder poder)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Poder_SET";

            var parametros = new
            {
                IdPoder = poder.IdPoder,
                IdRepresentante = poder.IdRepresentante,
                IdRepresentado = poder.IdRepresentado,
                IdAsamblea = poder.IdAsamblea,
                Observacion = poder.Observacion,
                idApartamentoRepresentante = poder.idApartamentoRepresentante,
                IdApartamentoRepresentado = poder.IdApartamentoRepresentado,
                Estado = 1,
                Action = poder.IdPoder == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);

        }

        public async Task<bool> ValidarPoder(int idAsamblea, string idUsuario)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Validar_Poder";

            var parametros = new
            {
                IdAsamblea = idAsamblea,
                IdUsuario = idUsuario
            };

            return await db.QuerySingleAsync<bool>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

    }
}



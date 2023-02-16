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
    public class OpcionPreguntaService : IOpcionPreguntaService
    {
        private readonly SqlConfiguration configuration;

        public OpcionPreguntaService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }
        public async Task<int> DelOpcionPregunta(int idOpcionpregunta)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Opcionpregunta_SET";

            var parametros = new
            {
                IdOpcionPregunta = idOpcionpregunta,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<OpcionPregunta> GetOpcionPregunta(int idOpcionpregunta)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Opcionpregunta_GET_DT";

            var parametros = new
            {
                IdOpcionPregunta = idOpcionpregunta
            };

            return await db.QuerySingleAsync<OpcionPregunta>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<OpcionPregunta>> GetOpcionPreguntas(int? idPregunta)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Opcionpregunta_GET_MS";

            var parametros = new
            {
                IdPregunta = idPregunta
            };

            return await db.QueryAsync<OpcionPregunta>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetOpcionPregunta(OpcionPregunta opcionpregunta)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Opcionpregunta_SET";

            var parametros = new
            {
                IdOpcionPregunta = opcionpregunta.IdOpcionpregunta,
                IdPregunta = opcionpregunta.IdPregunta,
                Opcion = opcionpregunta.Opcion,
                Estado = opcionpregunta.Estado,
                Action = opcionpregunta.IdOpcionpregunta == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

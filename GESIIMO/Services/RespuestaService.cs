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
    public class RespuestaService : IRespuestaService
    {
        private readonly SqlConfiguration configuration;

        public RespuestaService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public Task<Respuesta> GetRespuesta(int idRespuesta)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SetRespuesta(Respuesta respuesta)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Insert_Respuesta";

            var parametros = new
            {
                idOpcionPregunta = respuesta.idOpcionPregunta,
                idAccesoAsamblea = respuesta.idAccesoAsamblea

            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Votacion>> GetVotacion(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_Votaciones";

            var parametros = new
            {
                IdAsamblea = idAsamblea
            };

            return await db.QueryAsync<Votacion>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

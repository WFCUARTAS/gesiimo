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
    public class PreguntaService : IPreguntaService
    {
        private readonly SqlConfiguration configuration;

        public PreguntaService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }
        public async Task<int> DelPregunta(int idPregunta)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_BorraPregunta";

            var parametros = new
            {
                IdPregunta = idPregunta
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Pregunta> GetPregunta(int idPregunta)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Pregunta_GET_DT";

            var parametros = new
            {
                IdPregunta = idPregunta
            };

            return await db.QuerySingleAsync<Pregunta>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Pregunta>> GetPreguntas(int idCuestionario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Pregunta_GET_MS";

            var parametros = new
            {
                IdCuestionario = idCuestionario
            };

            try
            {
                return await db.QueryAsync<Pregunta>(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> SetPregunta(Pregunta pregunta)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Pregunta_SET";

            var parametros = new
            {
                IdPregunta = pregunta.IdPregunta,
                IdCuestionario = pregunta.IdCuestionario,
                TipoPregunta = pregunta.TipoPregunta,
                PreguntaDescripcion = pregunta.PreguntaDescripcion,
                TipoGrafica = pregunta.TipoGrafica,
                Estado = pregunta.Estado,
                NRespuesta = pregunta.NRespuesta,
                Action = pregunta.IdPregunta == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Pregunta>> GetPreguntasRespuesta(int idAsamblea, int idAccesoAsamblea)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_EstadoRespuesta";

            var parametros = new
            {
                idAsamblea = idAsamblea,
                idAccesoAsamblea = idAccesoAsamblea
            };

            try
            {
                return await db.QueryAsync<Pregunta>(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> GetContestoPregunta(int idPregunta, int idAccesoAsamblea)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_ContestoPregunta";

            var parametros = new
            {
                idPregunta = idPregunta,
                idAccesoAsamblea = idAccesoAsamblea
            };

            return await db.QuerySingleAsync<bool>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> ActivarPregunta(Pregunta pregunta)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Pregunta_SET";

            var parametros = new
            {
                IdPregunta = pregunta.IdPregunta,
                IdCuestionario = pregunta.IdCuestionario,
                TipoPregunta = pregunta.TipoPregunta,
                PreguntaDescripcion = pregunta.PreguntaDescripcion,
                TipoGrafica = pregunta.TipoGrafica,
                Estado = pregunta.Estado,
                NRespuesta = pregunta.NRespuesta,
                Action = "ACT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

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
    public class AsambleaService : IAsambleaService
    {
        private readonly SqlConfiguration configuration;

        public AsambleaService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<Asamblea> GetAsamblea(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asamblea_GET_DT";

            var parametros = new
            {
                IdAsamblea = idAsamblea
            };

            return await db.QuerySingleAsync<Asamblea>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Asamblea>> GetAsambleas(int? idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asamblea_GET_MS";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            return await db.QueryAsync<Asamblea>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetAsamblea(Asamblea asamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asamblea_SET";

            var parametros = new
            {
                IdAsamblea = asamblea.IdAsamblea,
                IdConjunto = asamblea.IdConjunto,
                Descripcion = asamblea.Descripcion,
                FechaInicio = asamblea.FechaInicio,
                FechaFin = asamblea.FechaFin,
                Estado = asamblea.Estado,
                EnvioMasivo = asamblea.EnvioMasivo,
                UrlZoom = asamblea.UrlZoom,
                OrdenDia = asamblea.OrdenDia,
                FechaInicioReal = asamblea.FechaInicioReal,
                Action = "PUT" 
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Quorum>> GetQuorum(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_Quorum";

            var parametros = new
            {
                idAsamblea = idAsamblea
            };

            return await db.QueryAsync<Quorum>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
        public async Task<int> SetQuorum(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Insert_Quorum";

            var parametros = new
            {
                idAsamblea = idAsamblea
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
        public async Task<Quorum> GetQuorumInforme(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_QuorumInforme";

            var parametros = new
            {
                IdAsamblea = idAsamblea
            };

            return await db.QuerySingleAsync<Quorum>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
        public async Task<Asamblea> SetNewAsamblea(Asamblea asamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Asamblea_SET";

            var parametros = new
            {
                IdAsamblea = asamblea.IdAsamblea,
                IdConjunto = asamblea.IdConjunto,
                Descripcion = asamblea.Descripcion,
                FechaInicio = asamblea.FechaInicio,
                FechaFin = asamblea.FechaFin,
                Estado = asamblea.Estado,
                EnvioMasivo = asamblea.EnvioMasivo,
                UrlZoom = asamblea.UrlZoom,
                OrdenDia = asamblea.OrdenDia,
                FechaInicioReal = asamblea.FechaInicioReal,
                Action = "POST"
            };

            return await db.QuerySingleAsync<Asamblea>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Archivo>> BorrarDatos(int idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_BorraDatos";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            return await db.QueryAsync<Archivo>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

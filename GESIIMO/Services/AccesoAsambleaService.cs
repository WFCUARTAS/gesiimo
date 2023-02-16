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
    public class AccesoAsambleaService : IAccesoAsambleaService
    {
        private readonly SqlConfiguration configuration;

        public AccesoAsambleaService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<AccesoAsamblea>> GetAccesoAsamblea(int idAsamblea, string idUsuario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_AccesoAsamblea_GET_DT";

            var parametros = new
            {
                IdAsamblea = idAsamblea,
                IdUsuario = idUsuario
            };

            return await db.QueryAsync<AccesoAsamblea>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public Task<IEnumerable<AccesoAsamblea>> GetAccesosActivos(int idAsamblea)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SetAccesoAsamblea(AccesoAsamblea acceso)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Insert_AccesoAsamblea";

            var parametros = new
            {
                IdAsamblea = acceso.idAsamblea,
                IdUsuario = acceso.IdUsuario,
                TerminosCondiciones = acceso.TerminosCondiciones
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> UpdateConexion(AccesoAsamblea acceso)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Update_Conexion_AccesoAsamblea";

            var parametros = new
            { 
                IdAsamblea = acceso.idAsamblea,
                IdUsuario = acceso.IdUsuario,
                Estado = acceso.EstadoConexion,
                Action = "PUT"
            };
            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

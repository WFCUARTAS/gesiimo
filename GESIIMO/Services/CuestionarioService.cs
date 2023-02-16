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
    public class CuestionarioService : ICuestionarioService
    {
        private readonly SqlConfiguration configuration;

        public CuestionarioService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<int> DelCuestionario(int idCuestionario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Cuestionario_SET";

            var parametros = new
            {
                IdCuestionario = idCuestionario,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Cuestionario_GET_DT";

            var parametros = new
            {
                IdCuestionario = idCuestionario
            };

            return await db.QuerySingleAsync<Cuestionario>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Cuestionario>> GetCuestionarios(int idAsamblea)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Cuestionario_GET_MS";

            var parametros = new
            {
                IdAsamblea = idAsamblea
            };

            try
            {
                return await db.QueryAsync<Cuestionario>(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> SetCuestionario(Cuestionario cuestionario)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Cuestionario_SET";

            var parametros = new
            {
                IdCuestionario = cuestionario.IdCuestionario,
                IdAsamblea = cuestionario.IdAsamblea,
                Descripcion = cuestionario.Descripcion,
                Estado = cuestionario.Estado,
                Action = cuestionario.IdCuestionario == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

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
    public class ConexionService : IConexionService
    {
        private readonly SqlConfiguration configuration;

        public ConexionService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }
        public Task<int> DelAsamblea(int idAsamblea)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Conexion>> GetConexiones(int? IdAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_Conexiones";

            var parametros = new
            {
                IdAsamblea = IdAsamblea
            };

            return await db.QueryAsync<Conexion>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

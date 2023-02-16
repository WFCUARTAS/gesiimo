using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class PropietarioService : IPropietarioService
    {
        private readonly SqlConfiguration configuration;

        public PropietarioService (SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<Propietario>> GetPropietarios(int? idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Propietario_GET_MS";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            return await db.QueryAsync<Propietario>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Propietario> GetPropietario(int idPropietario)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Propietario_GET_DT";

            var parametros = new
            {
                IdPropietario = idPropietario
            };

            return await db.QuerySingleAsync<Propietario>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Propietario> GetPropietarioUsuario(string idUsuario)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_Propietario";

            var parametros = new
            {
                IdUsuario = idUsuario
            };

            return await db.QuerySingleAsync<Propietario>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetPropietario(Propietario propietario)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Propietario_SET";
            var parametros = new
            {
                IdPropietario = propietario.IdPropietario,
                Nombre = propietario.Nombre,
                Apellido = propietario.Apellido,
                Email = propietario.Email,
                Celular = propietario.Celular,
                Action = propietario.IdPropietario == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> DelPropietario(int idPropietario)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Propietario_SET";
            var parametros = new
            {
                IdPropietario = idPropietario,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);

        }

        
    }
}

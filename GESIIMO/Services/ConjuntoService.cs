using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class ConjuntoService : IConjuntoService
    {
        private readonly SqlConfiguration configuration;

        public ConjuntoService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<Conjunto>> GetConjuntos(int idDepartamento, int idCiudad)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_GET_MS";

            var parametros = new
            {
                idDepartamento = idDepartamento,
                IdCiudad = idCiudad
            };

            return await db.QueryAsync<Conjunto>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Conjunto> GetConjunto(int idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_GET_DT";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            return await db.QuerySingleAsync<Conjunto>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetConjunto(Conjunto conjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_SET";

            var parametros = new
            {
                IdConjunto = conjunto.IdConjunto,
                IdCiudad = conjunto.IdCiudad,
                Nombre = conjunto.Nombre,
                Direccion = conjunto.Direccion,
                Telefono = conjunto.Telefono,
                Celular = conjunto.Celular,
                Administrador = conjunto.Administrador,
                Logo = conjunto.Logo,
                Email = conjunto.Email,
                Estado = conjunto.Estado,
                Action = conjunto.IdConjunto == 0 ? "POST" : "PUT"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> DelConjunto(int idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_SET";

            var parametros = new
            {
                IdConjunto = idConjunto,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EnvioCorreo>> GetCorreos(Asamblea asamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Return_Correos";

            var parametros = new
            {
                IdConjunto = asamblea.IdConjunto,
                IdAsamblea = asamblea.IdAsamblea
            };

            return await db.QueryAsync<EnvioCorreo>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

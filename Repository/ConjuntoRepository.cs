using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GESIIMO.Model;
using GESIIMO.Repository.Interfaces;

namespace GESIIMO.Repository
{
    public class ConjuntoRepository : IConjuntoRepository
    {
        private string ConnectionString;

        public ConjuntoRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Conjunto>> GetConjuntos(int? idCiudad)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_GET_MS";

            var parametros = new
            {
                IdCiudad = idCiudad
            };

            return await db.QueryAsync<Conjunto>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<Conjunto> GetConjunto(int idConjunto)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_GET_DT";

            var parametros = new
            {
                IdConjunto = idConjunto
            };

            return await db.QuerySingleAsync<Conjunto>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetConjunto(string action, Conjunto conjunto)
        {
            using SqlConnection db = dbConnection();
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
                Action = action
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> DelConjunto(int idConjunto)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Conjunto_SET";

            var parametros = new
            {
                IdConjunto = idConjunto,
                Action = "DEL"
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }

    }
}

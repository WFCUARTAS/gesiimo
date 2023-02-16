using Dapper;
using GESIIMO.Model;
using GESIIMO.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Repository
{
    public class ListaRepository : IListaRepository
    {
        private string ConnectionString;

        public ListaRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<IEnumerable<Lista>> GetLista(string nombre, int? id, bool? estado)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Lista_GET";

            var parametros = new
            {
                Nombre = nombre,
                Id = id,
                Estado = estado
            };

            return await db.QueryAsync<Lista>(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

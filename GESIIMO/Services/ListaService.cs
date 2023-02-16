using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GESIIMO.Services
{
    public class ListaService : IListaService
    {
        private readonly SqlConfiguration configuration;

        public ListaService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<Lista>> GetLista(string nombre, int? id, string codNovedad, bool? estado)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Lista_GET";

            var parametros = new
            {
                Nombre = nombre,
                Id = id,
                CodNovedad = codNovedad,
                Estado = estado
            };

            try
            {
                return await db.QueryAsync<Lista>(sql, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                return new List<Lista>();
            }
        }
    }
}
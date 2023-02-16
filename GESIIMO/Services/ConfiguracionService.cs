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
    public class ConfiguracionService : IConfiguracionService
    {
        private readonly SqlConfiguration configuration;

        public ConfiguracionService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<Configuracion> GetConfiguracion(string Descripcion)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Configuracion_GET";

            var parametros = new
            {
                Descripcion = Descripcion
            };

            return await db.QuerySingleAsync<Configuracion>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<int> SetConfiguracion(Configuracion configuracion)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_Configuracion_SET";

            var parametros = new
            {
                IdConfiguracion =configuracion.IdConfiguracion,
                Descripcion = configuracion.Descripcion,
                Action = configuracion.IdConfiguracion == 0 ? "POST" : "PUT",
                Parametro = configuracion.Parametro
                
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}

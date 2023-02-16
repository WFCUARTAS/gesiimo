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
    public class CargueJsonService : ICargueJsonService
    {
        private readonly SqlConfiguration configuration;

        public CargueJsonService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }
        public async Task<int> SetCargueJson(CargueJson carguejson)
        {
            using SqlConnection db = dbConnection();
            string sql = "dbo.sp_CargueMasivo";

            var parametros = new
            {
                DataJson = carguejson.DataJson,
                IdConjunto = carguejson.IdConjunto
            };

            return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
        }
    }
}
